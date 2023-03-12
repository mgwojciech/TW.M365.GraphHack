using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.M365.GraphHack.Lib.Services
{
    public interface ISubscriptionService<T> : IDisposable
    {
        event EventHandler<T> OnRemoteUpdate;
        Task PushUpdate(T updateBody);
        Task RegisterUpdateSubscription(string name, T entity);
        Task RegisterToExistingFile(string fileId);
        void SuspendListener();
    }
}
