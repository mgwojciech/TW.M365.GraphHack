using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace TW.M365.GraphHack.Lib.Graph.Model
{
    public class BatchResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("body")]
        public JsonElement Body { get; set; }
    }
    public class BatchResponseBody
    {
        [JsonPropertyName("responses")]
        public List<BatchResponse> BatchResponses { get; set; }
    }
}
