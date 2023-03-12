using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TW.M365.GraphHack.Lib.Authentication;

namespace TW.M365.GraphHack.ViewModels
{
	public partial class LoginPageViewModel : ObservableObject
    {
        protected IAADClientAuthenticator Auth;

        public LoginPageViewModel(IAADClientAuthenticator auth)
		{
            Auth = auth;
		}

        [RelayCommand]
        public async Task SignIn()
        {
            string res = await Auth.LogInUser();
            await Shell.Current.GoToAsync("///launch");
        }
    }
}

