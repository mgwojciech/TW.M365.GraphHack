﻿using System;
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
        public ImageSource userPhoto = "graphgiraffe.png";

        [ObservableProperty]
        public string userName = string.Empty;

        [ObservableProperty]
        public bool isRefreshing = false;


        public LaunchPageViewModel(IAADClientAuthenticator auth, IPeopleService peopleService, TicTacToeManager gameManager)
        {
            Auth = auth;
            PeopleService = peopleService;
            GameManager = gameManager;

            games = new ObservableCollection<Game>();
        }

        [RelayCommand]
        public async Task LoadGames()
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
        public async Task RefreshGames()
        {
            IsRefreshing = true;
            await LoadGames();
            IsRefreshing = false;
        }

        [RelayCommand]
        public async Task GetUser()
        {
            User user = await PeopleService.GetUser(null, false, true);
            UserName = user.DisplayName;
            UserPhoto = ImageSource.FromStream(() => user.AdditionalData["photoStream"] as Stream);
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
            await Auth.SignOutAsync();
            await Shell.Current.GoToAsync("///login");
        }
    }
}

