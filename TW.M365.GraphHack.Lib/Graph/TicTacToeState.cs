using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.M365.GraphHack.Lib.Graph
{
    public class TicTacToeState
    {
        public string GameId { get; set; }
        public List<int> User1Moves { get; set; } = new List<int>();
        public List<int> User2Moves { get; set; } = new List<int>();
    }
}
