using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TW.M365.GraphHack.Lib.Graph.Model
{
    public class BatchRequest
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("body")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull)]
        public HttpContent? Body { get; set; }
        [JsonPropertyName("method")]
        public string Method { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("headers")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string>? Headers { get; set; }

    }
    public class BatchRequestBody
    {
        [JsonPropertyName("requests")]
        public List<BatchRequest> BatchRequests { get; set; } = new List<BatchRequest>();
    }
}
