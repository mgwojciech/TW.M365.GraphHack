namespace TW.M365.GraphHack;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("login", typeof(LoginPage));
        Routing.RegisterRoute("launch", typeof(LaunchPage));
        Routing.RegisterRoute("main", typeof(MainPage));
    }
}
