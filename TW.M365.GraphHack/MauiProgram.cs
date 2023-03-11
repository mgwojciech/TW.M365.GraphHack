using Microsoft.Extensions.Logging;
using TW.M365.GraphHack.Lib.Authentication;
using TW.M365.GraphHack.Lib.GameManager;
using TW.M365.GraphHack.Lib.Graph;
using TW.M365.GraphHack.Lib.HttpHandlers;
using TW.M365.GraphHack.Lib.Services;
using TW.M365.GraphHack.Views;

namespace TW.M365.GraphHack;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<IAADClientAuthenticator>(services =>
        {
            ClientAppInfo clientAppInfo = new ClientAppInfo()
            {
                ClientId = AppConstants.ClientId,
                RedirectUri = PlatformConfig.Instance.RedirectUri,
                Authority = "https://login.microsoftonline.com/organizations"
            };

            return new AuthenticationService(clientAppInfo, PlatformConfig.Instance.ParentWindow)
            {
                Scopes = AppConstants.Scopes
            };

        });
        builder.Services.AddSingleton<HttpClient>(services =>
        {
            IAADClientAuthenticator aadClientAuthenticator = services.GetRequiredService<IAADClientAuthenticator>();
            return new HttpClient(new GraphHttpHandler(aadClientAuthenticator));
        });
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<IPeopleService, GraphPeopleService>();
        builder.Services.AddSingleton<TicTacToeManager>();
        builder.Services.AddSingleton<ISubscriptionService<TicTacToeState>, GraphListSubscriptionService<TicTacToeState>>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
