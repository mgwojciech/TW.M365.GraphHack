using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TW.M365.GraphHack.Lib.Graph.Model;

namespace TW.M365.GraphHack.Lib.Services
{
    public class GraphFileService : IFileService
    {
        HttpClient _client;
        public GraphFileService(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> GetFileContent<T>(string id)
        {
            using (var folderContent = await _client.GetAsync($"https://graph.microsoft.com/v1.0/sites/root/drive/items/{id}"))
            {
                var response = await folderContent.Content.ReadFromJsonAsync<FileResponse>();
                using(var fileContentResponse = await _client.GetAsync(response.DownloadUrl))
                {
                    return await fileContentResponse.Content.ReadFromJsonAsync<T>();
                }
            }
        }

        public async Task<List<FileResponse>> GetFilesInFolder(string folderName)
        {
            using(var rootResponse = await _client.GetAsync($"https://graph.microsoft.com/v1.0/sites/root/drive/root/children?$filter=name eq '{folderName}'"))
            {
                var rootFolderContents = await rootResponse.Content.ReadFromJsonAsync<CollectionResponse<BaseItem>>();
                if(rootFolderContents is null || rootFolderContents.Value.Count == 0)
                {
                    return new List<FileResponse>();
                }
                var folder = rootFolderContents.Value.FirstOrDefault();
                if(folder == null)
                {
                    return new List<FileResponse>();
                }
                using(var folderContent = await _client.GetAsync($"https://graph.microsoft.com/v1.0/sites/root/drive/items/{folder.Id}/children"))
                {
                    var response = await folderContent.Content.ReadFromJsonAsync<CollectionResponse<FileResponse>>();
                    return response.Value;
                }
            }
        }
    }
}
