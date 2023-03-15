using System;
using System.Net.Http.Json;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Graph;
using TW.M365.GraphHack.Lib.GameManager;
using TW.M365.GraphHack.Lib.Graph;

namespace TW.M365.GraphHack.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        TicTacToeManager _gameManager;
        public MainPageViewModel(TicTacToeManager gameManager)
        {
            _gameManager = gameManager;
            if (_gameManager.OnGoing)
            {
                AssignUserTiles(_gameManager.GameState);
            }
        }

        [RelayCommand]
        public async Task LoadGame()
        {
            if (_gameManager.OnGoing)
            {
                AssignUserTiles(_gameManager.GameState);
                _gameManager.OnOpponentMoved += _gameManager_OnOpponentMoved;
            }
        }

        [ObservableProperty]
        private string play0 = string.Empty;

        [ObservableProperty]
        private string play1 = string.Empty;

        [ObservableProperty]
        private string play2 = string.Empty;

        [ObservableProperty]
        private string play3 = string.Empty;

        [ObservableProperty]
        private string play4 = string.Empty;

        [ObservableProperty]
        private string play5 = string.Empty;

        [ObservableProperty]
        private string play6 = string.Empty;

        [ObservableProperty]
        private string play7 = string.Empty;

        [ObservableProperty]
        private string play8 = string.Empty;

        [RelayCommand]
        public async Task EndGame()
        {
            await Shell.Current.GoToAsync("///launch");
        }

        [RelayCommand]
        private async Task Play(string number)
        {
            int tileIndex = int.Parse(number);
            int tileNumber = tileIndex + 1;
            if (!_gameManager.OnGoing)
            {
                updateTile(tileNumber, "X");
                await _gameManager.StartGame(tileNumber);
                _gameManager.OnOpponentMoved += _gameManager_OnOpponentMoved;
            }
            else
            {
                if (await _gameManager.MakeAMove(tileNumber))
                {
                }
            }
            AssignUserTiles(_gameManager.GameState);
        }

        protected void AssignUserTiles(TicTacToeState state)
        {
            foreach(int user1Tile in state.User1Moves)
            {
                updateTile(user1Tile, "X");
            }
            foreach (int user2Tile in state.User2Moves)
            {
                updateTile(user2Tile, "O");

            }
        }

        private void updateTile(int user1Tile, string mark)
        {
            PropertyInfo propInfo = this.GetType().GetProperty($"Play{user1Tile - 1}");
            propInfo.SetValue(this, mark);
        }

        private void _gameManager_OnOpponentMoved(object sender, Lib.Graph.TicTacToeState e)
        {
            AssignUserTiles(e);
        }
    }
}

