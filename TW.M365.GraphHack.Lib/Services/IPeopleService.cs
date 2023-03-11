using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.M365.GraphHack.Lib.Services
{
    public interface IPeopleService
    {
        Task<User> GetUser(string userId = null, bool loadPresence = false, bool loadPhoto = false);
    }
}
