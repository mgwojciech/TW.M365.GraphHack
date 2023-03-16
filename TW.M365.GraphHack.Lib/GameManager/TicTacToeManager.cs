using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW.M365.GraphHack.Lib.Graph;
using TW.M365.GraphHack.Lib.Services;

namespace TW.M365.GraphHack.Lib.GameManager
{
    public class TicTacToeManager
    {
        private ISubscriptionService<TicTacToeState> _subscriptionService;
        private IFileService _fileService;
        public TicTacToeState GameState { get; set; }
        public event EventHandler<TicTacToeState> OnOpponentMoved;
        public bool OnGoing { get; set; } = false;
        public bool PlayerWon { get; set; } = false;
        public bool CurrentPlayerMove { get; set; } = false;
        protected bool UserIsUser1 { get; set; } = false;
        public TicTacToeManager(ISubscriptionService<TicTacToeState> subscriptionService, IFileService fileService)
        {
            _subscriptionService = subscriptionService;
            _fileService = fileService;
        }
        public async Task StartGame(int tileNumber)
        {
            UserIsUser1 = true;
            string gameId = Guid.NewGuid().ToString();
            GameState = new TicTacToeState()
            {
                GameId = gameId,
                User1Moves = new List<int>() { tileNumber }
            };
            await _subscriptionService.RegisterUpdateSubscription($"TicTacToe/{gameId}", GameState);
            _subscriptionService.OnRemoteUpdate += _subscriptionService_OnRemoteUpdate;
            OnGoing = true;
        }

        private void _subscriptionService_OnRemoteUpdate(object? sender, TicTacToeState e)
        {
            GameState = e;
            CurrentPlayerMove = true;
            _subscriptionService.SuspendListener();
            if(OnOpponentMoved is not null)
            {
                OnOpponentMoved.Invoke(this, GameState);
            }
        }
        public async Task<List<string>> GetAvailableGames()
        {
            var files = await _fileService.GetFilesInFolder("TicTacToe");
            return files.Where(file => file.ETag.Contains(",1")).Select(file => file.Id).ToList();
        }
        public async Task JoinGame(string gameId)
        {
            UserIsUser1 = false;
            CurrentPlayerMove = true;
            OnGoing = true;
            await _subscriptionService.RegisterToExistingFile(gameId);
            GameState = await _fileService.GetFileContent<TicTacToeState>(gameId);
            _subscriptionService.OnRemoteUpdate += _subscriptionService_OnRemoteUpdate;

        }
        /// <summary>
        /// Returns true if move is valid
        /// </summary>
        /// <param name="tileNumber"></param>
        /// <returns></returns>
        public bool MakeAMove(int tileNumber)
        {
            if (GameState.User1Moves.Contains(tileNumber))
            {
                return false;
            }
            if (GameState.User2Moves.Contains(tileNumber))
            {
                return false;
            }
            if (UserIsUser1)
            {
                GameState.User1Moves.Add(tileNumber);
            }
            else
            {
                GameState.User2Moves.Add(tileNumber);
            }
            return true;
        }

        /// <summary>
        /// Updates backend
        /// </summary>
        /// <returns></returns>
        public async Task PushUpdate()
        {
            await _subscriptionService.PushUpdate(GameState);
        }
    }
}
