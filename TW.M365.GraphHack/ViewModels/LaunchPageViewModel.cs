using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Graph;
using TW.M365.GraphHack.Lib.Authentication;
using TW.M365.GraphHack.Lib.Services;

namespace TW.M365.GraphHack.ViewModels
{
    public class Game
    {
        public string FileName { get; set; }
    }

    public partial class LaunchPageViewModel : ObservableObject
    {
        protected IAADClientAuthenticator Auth;
        protected IPeopleService PeopleService;

        public ObservableCollection<Game> games { get; set; }

        [ObservableProperty]
        public string userName = string.Empty;

        public LaunchPageViewModel(IAADClientAuthenticator auth, IPeopleService peopleService)
		{
            Auth = auth;
            PeopleService = peopleService;

            games = new ObservableCollection<Game>()
            {
                new Game { FileName = "gameone.json" },
                new Game { FileName = "gametwo.json" },
                new Game { FileName = "gamethree.json" }
            };
        }

        [RelayCommand]
        public async Task GetUser()
        {
            User user = await PeopleService.GetUser();
            UserName = user.DisplayName;
        }

        [RelayCommand]
        public async Task StartGame()
        {
            await Shell.Current.GoToAsync("///main");
        }

        [RelayCommand]
        public async Task SignOut()
        {
            await Auth.SignOutAsync();
            await Shell.Current.GoToAsync("///login");
        }
    }
}

