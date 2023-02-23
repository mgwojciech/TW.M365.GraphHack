using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.M365.GraphHack.Lib.Authentication
{
    /// <summary>
    /// Stores information about Client App registration
    /// </summary>
    public class ClientAppInfo
    {
        /// <summary>
        /// AppId of the app registration (Check in AAD)
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// Security group name used by iOS Keychain
        /// </summary>
        public string SecurityGroup { get; set; }
        /// <summary>
        /// Redirect uri of the app registration (Check in AAD). By default "msal{ClientId}://auth"
        /// </summary>
        public string RedirectUri { get; set; }
        /// <summary>
        /// Client App authority set to https://login.microsoftonline.com/organizations for multi-tenant
        /// or to https://login.microsoftonline.com/{TenantId} for single tenant apps
        /// </summary>
        public string Authority { get; set; }
    }
}
