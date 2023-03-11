using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TW.M365.GraphHack.Lib.Services
{
    public class GraphPeopleService : IPeopleService
    {
        protected HttpClient GraphClient { get; }
        public GraphPeopleService(HttpClient graphClient)
        {
            GraphClient = graphClient;
        }
        public async Task<User> GetUser(string userId = null, bool loadPresence = false, bool loadPhoto = false)
        {
            string baseQuery = "https://graph.microsoft.com/v1.0/";
            if (String.IsNullOrEmpty(userId))
            {
                baseQuery += "me";
            }
            else
            {
                baseQuery += userId;
            }
            Task<HttpResponseMessage> meResponse = GraphClient.GetAsync(baseQuery);
            Task<HttpResponseMessage> myPhotoResponse = null;
            Task<HttpResponseMessage> myPresenceResponse = null;
            List<Task> tasks = new List<Task>()
            {
                meResponse
            };
            if (loadPhoto)
            {
                myPhotoResponse = GraphClient.GetAsync($"{baseQuery}/photo/$value");
                tasks.Add(myPhotoResponse);
            }
            if (loadPresence)
            {
                myPresenceResponse = GraphClient.GetAsync($"{baseQuery}/presence");
                tasks.Add(myPresenceResponse);
            }
            await Task.WhenAll(tasks.ToArray());
            User result = meResponse.Result.Content.ReadFromJsonAsync<User>().Result;
            if(loadPresence)
            {
                result.Presence = myPresenceResponse.Result.Content.ReadFromJsonAsync<Presence>().Result;
            }
            if (loadPhoto)
            {
                result.AdditionalData.Add("photoBytes", myPhotoResponse.Result.Content.ReadAsByteArrayAsync().Result);
            }

            return result;
        }
    }
}
