using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace TW.M365.GraphHack
{
    [Activity(Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataHost = "auth",
        DataScheme = "msal1f3e3046-7be4-43ac-b80e-d414336bdc76")]
    public class MsalActivity : BrowserTabActivity
    {
    }
}
