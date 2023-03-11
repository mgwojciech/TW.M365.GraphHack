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
        protected TicTacToeState GameState { get; set; }
        public event EventHandler<TicTacToeState> OnOpponentMoved;
        public bool OnGoing { get; set; } = false;
        public bool PlayerWon { get; set; } = false;
        public bool CurrentPlayerMove { get; set; } = false;
        public TicTacToeManager(ISubscriptionService<TicTacToeState> subscriptionService)
        {
            _subscriptionService = _subscriptionService;
        }
        public async Task StartGame(int tileNumber)
        {
            string gameId = Guid.NewGuid().ToString();
            GameState = new TicTacToeState()
            {
                GameId = gameId,
                User1Moves = new List<int>() { tileNumber }
            };
            await _subscriptionService.RegisterUpdateSubscription(gameId, GameState);
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

        public async Task JoinGame(string gameId)
        {
            
        }
        /// <summary>
        /// Returns true if move is valid
        /// </summary>
        /// <param name="tileNumber"></param>
        /// <returns></returns>
        public async Task<bool> MakeAMove(int tileNumber)
        {
            if (GameState.User1Moves.Contains(tileNumber))
            {
                return false;
            }
            if (GameState.User2Moves.Contains(tileNumber))
            {
                return false;
            }
            GameState.User1Moves.Add(tileNumber);
            await _subscriptionService.PushUpdate(GameState);
            return true;
        }
    }
}
