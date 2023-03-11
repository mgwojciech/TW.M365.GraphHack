using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TW.M365.GraphHack.Lib.Graph.Model
{
    public class FileResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ETag { get; set; }
        [JsonPropertyName("@microsoft.graph.downloadUrl")]
        public string DownloadUrl { get; set; }
        public CreatedBy CreatedBy { get; set; }
        
    }
    public class CreatedBy
    {
        public CreatedByUser User { get; set; }
    }
    public class CreatedByUser
    {
        public string Email { get; set; }
    }
}
