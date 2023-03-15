using System;
namespace TW.M365.GraphHack
{
    internal static class AppConstants
    {
        // ClientID of the application in (ms sample testing)
        internal const string ClientId = "1f3e3046-7be4-43ac-b80e-d414336bdc76"; // TODO - Replace with your client Id. And also replace in the AndroidManifest.xml

        // TenantID of the organization (ms sample testing)
        internal const string TenantId = "common"; // TODO - Replace with your TenantID. And also replace in the AndroidManifest.xml

        /// <summary>
        /// Scopes defining what app can access in the graph
        /// </summary>
        internal static string[] Scopes = {
            "https://graph.microsoft.com/User.Read",
            "https://graph.microsoft.com/Calendars.Read" ,
            "https://graph.microsoft.com/Calendars.Read.Shared" ,
            "https://graph.microsoft.com/Sites.Read.All" ,
            "https://graph.microsoft.com/Team.ReadBasic.All",
            "https://graph.microsoft.com/People.Read",
            "https://graph.microsoft.com/Files.ReadWrite.All"
        };
    }
}

