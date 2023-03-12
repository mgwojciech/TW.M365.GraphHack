using TW.M365.GraphHack.ViewModels;

namespace TW.M365.GraphHack;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginPageViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}

    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }
}
