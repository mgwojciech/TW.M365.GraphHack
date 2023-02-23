using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace TW.M365.GraphHack
{
    [Activity(Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataHost = "auth",
        DataScheme = "msal99bf0f5c-99de-43fd-9815-036a1ebcb01c")]
    public class MsalActivity : BrowserTabActivity
    {
    }
}
