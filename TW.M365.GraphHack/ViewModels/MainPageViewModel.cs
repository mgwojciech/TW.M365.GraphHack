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
        public async Task SignInCommand()
        {
        }

        [RelayCommand]
        private async Task Play(string number)
        {
            int tileIndex = int.Parse(number);
            int tileNumber = tileIndex + 1;
            if (!_gameManager.OnGoing)
            {
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
                PropertyInfo propInfo = this.GetType().GetProperty($"Play{user1Tile - 1}");
                propInfo.SetValue(this, "X");
            }
            foreach (int user2Tile in state.User2Moves)
            {
                PropertyInfo propInfo = this.GetType().GetProperty($"Play{user2Tile - 1}");
                propInfo.SetValue(this, "O");
            }
        }

        private void _gameManager_OnOpponentMoved(object sender, Lib.Graph.TicTacToeState e)
        {
            AssignUserTiles(e);
        }
    }
}

