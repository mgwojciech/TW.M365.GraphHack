﻿using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace TW.M365.GraphHack.Lib.Authentication
{
    public class AuthenticationService : IAADClientAuthenticator
    {
        protected IPublicClientApplication PublicClientApp { get; }
        public string[] Scopes { get; set; } = new string[] {
            "https://graph.microsoft.com/User.Read",
            "https://graph.microsoft.com/Calendars.Read" ,
            "https://graph.microsoft.com/Calendars.Read.Shared" ,
            "https://graph.microsoft.com/Sites.Read.All" ,
            "https://graph.microsoft.com/Team.ReadBasic.All",
            "https://graph.microsoft.com/Presence.Read",
            "https://graph.microsoft.com/People.Read" };
        protected object ParentWindow { get; set; }
        public string AadDomain { get; set; }
        private object lockObj = new object();

        public AuthenticationService(ClientAppInfo appInfo, object parentWindow)
        {
            ParentWindow = parentWindow;
            PublicClientApp = PublicClientApplicationBuilder.Create(appInfo.ClientId)
        .WithRedirectUri(appInfo.RedirectUri)
        .WithAuthority(appInfo.Authority)
        .Build();
        }

        public async Task<string> GetToken()
        {
            try
            {
                var accounts = await PublicClientApp.GetAccountsAsync();
                if (String.IsNullOrEmpty(AadDomain) && accounts.Count() > 0)
                {
                    IAccount defaultAccount = accounts.FirstOrDefault();
                    AadDomain = defaultAccount.GetTenantProfiles().FirstOrDefault().TenantId;
                }
                if (accounts.Count() == 0)
                {
                    return await LogInUser();
                }
                IAccount account = accounts.FirstOrDefault(acc => acc.GetTenantProfiles().Any(tenant => tenant.TenantId == AadDomain));

                AuthenticationResult authResult = await PublicClientApp.AcquireTokenSilent(Scopes, account).ExecuteAsync();
                // Store the access token securely for later use.
                return authResult.AccessToken;

            }
            catch (MsalUiRequiredException)
            {
                try
                {
                    return await LogInUser();
                }
                catch (Exception ex2)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<string> LogInUser()
        {
            lock (lockObj)
            {
                var authResult = PublicClientApp.AcquireTokenInteractive(Scopes)
                                        //.WithPrompt(Prompt.ForceLogin)
                                        .WithSystemWebViewOptions(new SystemWebViewOptions())
                                        .WithParentActivityOrWindow(ParentWindow)
                                        .ExecuteAsync()
                                        .ConfigureAwait(false).GetAwaiter().GetResult();
                AadDomain = authResult.TenantId;
                return authResult.AccessToken;
            }
        }

        public async Task AuthenticateRequest(HttpRequestMessage request)
        {
            string token = await GetToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
