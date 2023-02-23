using Microsoft.Extensions.Logging;
using TW.M365.GraphHack.Lib.Authentication;
using TW.M365.GraphHack.Lib.HttpHandlers;

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
                ClientId = "99bf0f5c-99de-43fd-9815-036a1ebcb01c",
                RedirectUri = "msal99bf0f5c-99de-43fd-9815-036a1ebcb01c://auth",
                Authority = "https://login.microsoftonline.com/organizations"
            };
            AuthenticationService authService = new AuthenticationService(clientAppInfo, null);
#if ANDROID
		authService = new AuthenticationService(clientAppInfo, Platform.CurrentActivity);
#endif
            return authService;
        });
		builder.Services.AddSingleton<HttpClient>(services =>
		{
            IAADClientAuthenticator aadClientAuthenticator = services.GetRequiredService<IAADClientAuthenticator>();
            return new HttpClient(new GraphHttpHandler(aadClientAuthenticator));
        });
		builder.Services.AddSingleton<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
