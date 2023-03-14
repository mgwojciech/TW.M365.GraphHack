using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Graph;
using TW.M365.GraphHack.Lib.Authentication;
using TW.M365.GraphHack.Lib.GameManager;
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
        protected TicTacToeManager GameManager { get; set; }

        public ObservableCollection<Game> games { get; set; }

        [ObservableProperty]
        public string userName = string.Empty;

        public LaunchPageViewModel(IAADClientAuthenticator auth, IPeopleService peopleService, TicTacToeManager gameManager)
        {
            Auth = auth;
            PeopleService = peopleService;
            GameManager = gameManager;

            games = new ObservableCollection<Game>()
            {
                new Game { FileName = "gameone.json" },
                new Game { FileName = "gametwo.json" },
                new Game { FileName = "gamethree.json" }
            };
            LoadGames();
        }

        protected async Task LoadGames()
        {
            var gameIds = await GameManager.GetAvailableGames();
            games.Clear();
            foreach (var gameId in gameIds)
            {
                games.Add(new Game()
                {
                    FileName = gameId
                });
            }
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
        public async Task JoinGame(string gameId)
        {
            await GameManager.JoinGame(gameId);
            await Shell.Current.GoToAsync("///main");
        }

        [RelayCommand]
        public async Task SignOut()
        {
            //await Auth.SignOutAsync();
            await Shell.Current.GoToAsync("///login");
        }
    }
}

