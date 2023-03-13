using TW.M365.GraphHack.ViewModels;

namespace TW.M365.GraphHack;

public partial class LaunchPage : ContentPage
{
    public LaunchPage(LaunchPageViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}
}
