using System;
using System.Linq.Expressions;
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
        private bool tie = false;

        [ObservableProperty]
        private bool youWon = false;

        [ObservableProperty]
        private bool gameEnd = false;

        [ObservableProperty]
        private bool isMyturn = true;

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

        [ObservableProperty]
        private bool win0 = false;
        [ObservableProperty]
        private bool win1 = false;
        [ObservableProperty]
        private bool win2 = false;
        [ObservableProperty]
        private bool win3 = false;
        [ObservableProperty]
        private bool win4 = false;
        [ObservableProperty]
        private bool win5 = false;
        [ObservableProperty]
        private bool win6 = false;
        [ObservableProperty]
        private bool win7 = false;
        [ObservableProperty]
        private bool win8 = false;

        [RelayCommand]
        public async Task EndGame()
        {
            await Shell.Current.GoToAsync("///launch");
        }

        [RelayCommand]
        private async Task Play(string number)
        {
            if (IsMyturn && !GameEnd)
            {
                int tileIndex = int.Parse(number);
                int tileNumber = tileIndex + 1;
                if (!_gameManager.OnGoing)
                {
                    IsMyturn = false;
                    updateTile(tileNumber, "X");
                    await _gameManager.StartGame(tileNumber);
                    _gameManager.OnOpponentMoved += _gameManager_OnOpponentMoved;
                }
                else
                {
                    if (_gameManager.MakeAMove(tileNumber))
                    {
                        IsWinner();
                        IsMyturn = false;
                        AssignUserTiles(_gameManager.GameState);

                        await _gameManager.PushUpdate();
                    }
                }
            }
        }

        private void IsWinner()
        {
            var winningLine = GameHelper.IsWinningMoveSet(_gameManager.GameState.User1Moves);
            var winningLine2 = GameHelper.IsWinningMoveSet(_gameManager.GameState.User2Moves);

            if (winningLine.Length > 0 || winningLine2.Length > 0)
            {
                GameEnd = true;
                if (GameEnd && IsMyturn) {
                    YouWon = true;
                }
                foreach (var user1Tile in winningLine.Split(','))
                {
                    markWinningRow(user1Tile);
                }
                foreach (var user1Tile in winningLine2.Split(','))
                {
                    markWinningRow(user1Tile);
                }
            }
            var total = _gameManager.GameState.User1Moves.ToArray().Length + _gameManager.GameState.User2Moves.ToArray().Length;
            if (!GameEnd && total == 9) {
                Tie = true;
                GameEnd = true;
            }
        }

        private void markWinningRow(string user1Tile)
        {
            switch (user1Tile)
            {
                case "1":
                    Win0 = true;
                    break;
                case "2":
                    Win1 = true;
                    break;
                case "3":
                    Win2 = true;
                    break;
                case "4":
                    Win3 = true;
                    break;
                case "5":
                    Win4 = true;
                    break;
                case "6":
                    Win5 = true;
                    break;
                case "7":
                    Win6 = true;
                    break;
                case "8":
                    Win7 = true;
                    break;
                case "9":
                    Win8 = true;
                    break;
                default:
                    break;
            }
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
            IsWinner();
            IsMyturn = true;
        }
    }
}

