using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using TW.M365.GraphHack.Lib.Graph.Model;

namespace TW.M365.GraphHack.Lib.Services
{
    public class GraphListSubscriptionService<T> : ISubscriptionService<T>
    {
        protected HttpClient GraphClient { get; }
        public string Site { get; set; } = "root";
        public event EventHandler<T> OnRemoteUpdate;
        protected string SubscriptionUrl { get; set; }
        private bool isChecking = false;
        protected FileResponse CheckingFile { get; set; }
        public GraphListSubscriptionService(HttpClient graphClient)
        {
            GraphClient = graphClient;
        }

        public async Task PushUpdate(T updateBody)
        {
            JsonContent fileContent = JsonContent.Create(updateBody);
            string createFileUrl = $"https://graph.microsoft.com/v1.0/sites/root/drive/items/{CheckingFile.Id}/content";
            using (var response = await GraphClient.PutAsync(createFileUrl, fileContent))
            {
                CheckingFile = await response.Content.ReadFromJsonAsync<FileResponse>();
            }
            isChecking = true;
            StartCheckingTheList();

        }

        public async Task RegisterUpdateSubscription(string name, T entity)
        {
            JsonContent fileContent = JsonContent.Create(entity);
            string createFileUrl = $"https://graph.microsoft.com/v1.0/sites/root/drive/root:/{name}.json:/content";
            using (var response = await GraphClient.PutAsync(createFileUrl, fileContent))
            {
                CheckingFile = await response.Content.ReadFromJsonAsync<FileResponse>();
            }
            isChecking = true;
            StartCheckingTheList();
        }

        protected async Task StartCheckingTheList()
        {
            while (isChecking)
            {
                Thread.Sleep(1000);
                string apiUrl = $"https://graph.microsoft.com/v1.0/sites/root/drive/items/{CheckingFile.Id}";
                using(var response = await GraphClient.GetAsync(apiUrl))
                {
                    FileResponse file = await response.Content.ReadFromJsonAsync<FileResponse>();
                    if(CheckingFile.ETag != file.ETag)
                    {
                        CheckingFile.ETag = file.ETag;
                        using(var fileResponse = await GraphClient.GetAsync(file.DownloadUrl))
                        {
                            T updatedContent = await fileResponse.Content.ReadFromJsonAsync<T>();
                            if(OnRemoteUpdate is not null)
                            {
                                OnRemoteUpdate(this, updatedContent);
                            }
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            if (isChecking)
            {
                isChecking = false;
            }
            GraphClient.DeleteAsync($"https://graph.microsoft.com/v1.0/sites/root/drive/items/{CheckingFile.Id}");
        }

        public void SuspendListener()
        {
            isChecking = false;
        }

        public async Task RegisterToExistingFile(string fileId)
        {
            using (var response = await GraphClient.GetAsync($"https://graph.microsoft.com/v1.0/sites/root/drive/items/{fileId}/"))
            {
                CheckingFile = await response.Content.ReadFromJsonAsync<FileResponse>();
            }
            isChecking = true;
            StartCheckingTheList();
        }
    }
}
