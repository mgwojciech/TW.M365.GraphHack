using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TW.M365.GraphHack.Lib.Authentication;
using TW.M365.GraphHack.Lib.Graph.Model;
using System.Text.Json;
using System.Net.Http.Headers;

namespace TW.M365.GraphHack.Lib.HttpHandlers
{
    public class GraphHttpHandler : HttpClientHandler
    {
        private IAADClientAuthenticator _aadClientAuthenticator;
        public Dictionary<string, HttpRequestMessage> BatchDictionary = new Dictionary<string, HttpRequestMessage>();
        protected Task<Task<HttpResponseMessage>>? BatchDelayTask;
        public string Version { get; set; } = "v1.0";
        public int BatchDelayInMilliseconds { get; set; } = 50;
        private BatchResponseBody? batchResponseBody;
        object _lockObj = new object();
        public GraphHttpHandler(IAADClientAuthenticator aadClientAuthenticator)
        {
            _aadClientAuthenticator = aadClientAuthenticator;
        }
        protected virtual Dictionary<string, string> GetHeaders(HttpHeaders headers)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var header in headers)
            {
                if (header.Key != "Authorization")
                {
                    result.Add(header.Key, header.Value.FirstOrDefault());
                }
            }
            if (result.Count > 0)
                return result;
            return null;
        }
        protected virtual async Task<HttpResponseMessage> GetBatchResponse(Task task)
        {
            BatchRequestBody body = new BatchRequestBody()
            {
                BatchRequests = BatchDictionary.Select(batch => new BatchRequest()
                {
                    Url = batch.Value.RequestUri.ToString().Split(Version)[1],
                    Id = batch.Key,
                    Body = batch.Value.Content,
                    Headers = GetHeaders(batch.Value.Headers),
                    Method = batch.Value.Method.ToString()
                }).ToList()
            };
            HttpRequestMessage batchRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"https://graph.microsoft.com/{Version}/$batch");
            await _aadClientAuthenticator.AuthenticateRequest(batchRequestMessage);
            batchRequestMessage.Content = JsonContent.Create(body);
            var batchTask = base.SendAsync(batchRequestMessage, CancellationToken.None);
            HttpResponseMessage result = await batchTask;
            batchResponseBody = JsonSerializer.Deserialize<BatchResponseBody>(result.Content.ReadAsStringAsync().Result);

            return result;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Guid requestGuid = Guid.NewGuid();
            lock (_lockObj)
            {
                BatchDictionary.TryAdd(requestGuid.ToString(), request);
                if (BatchDelayTask == null)
                {
                    BatchDelayTask = Task.Delay(BatchDelayInMilliseconds).ContinueWith(GetBatchResponse);
                }
            }
            try
            {
                await BatchDelayTask;
                HttpResponseMessage batchResponse = await BatchDelayTask.Result;
                var batchResult = batchResponseBody.BatchResponses.FirstOrDefault(r => r.Id == requestGuid.ToString());
                return new HttpResponseMessage((HttpStatusCode)batchResult.Status)
                {
                    Content = JsonContent.Create(batchResult.Body)
                };
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                BatchDictionary.Remove(requestGuid.ToString());
                if (BatchDictionary.Count == 0)
                {
                    BatchDelayTask = null;
                }
            }
        }
    }
}
