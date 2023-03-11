using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.M365.GraphHack.ViewModels
{
    public interface IPersonViewModel
    {
        Task<User> User { get; }
    }
}
