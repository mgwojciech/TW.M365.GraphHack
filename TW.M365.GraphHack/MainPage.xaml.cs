using TW.M365.GraphHack.ViewModels;

namespace TW.M365.GraphHack;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        (BindingContext as MainPageViewModel).SignInCommand();
    }
}

