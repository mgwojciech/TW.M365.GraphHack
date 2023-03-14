using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW.M365.GraphHack.Lib.Graph.Model;

namespace TW.M365.GraphHack.Lib.Services
{
    public interface IFileService
    {
        Task<List<FileResponse>> GetFilesInFolder(string folderName);
        Task<T> GetFileContent<T>(string id);
    }
}
