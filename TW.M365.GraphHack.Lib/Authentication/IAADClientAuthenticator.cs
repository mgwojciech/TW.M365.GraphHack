using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.M365.GraphHack.Lib.Authentication
{
    public interface IAADClientAuthenticator
    {
        string AadDomain { get; set; }
        Task AuthenticateRequest(HttpRequestMessage request);
        Task<string> LogInUser();
        Task SignOutAsync();
    }
}
