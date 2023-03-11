using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.M365.GraphHack.Lib.Graph.Model
{
    public class SocketIOResponse
    {
        public string Id { get; set; }
        public string NotificationUrl { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public string ClientState { get; set; }
    }
}
