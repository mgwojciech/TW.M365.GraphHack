using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW.M365.GraphHack.Lib.Services;

namespace TW.M365.GraphHack.ViewModels
{
    public class PersonViewModel : IPersonViewModel
    {
        protected IPeopleService PeopleService { get; }
        public PersonViewModel(IPeopleService peopleService)
        {
            PeopleService = peopleService;
        }
        private Task<User> _user;
        public Task<User> User
        {
            get
            {
                if (_user == null)
                {
                    _user = PeopleService.GetUser(null, false, true);
                }
                return _user;
            }
        }
    }
}
