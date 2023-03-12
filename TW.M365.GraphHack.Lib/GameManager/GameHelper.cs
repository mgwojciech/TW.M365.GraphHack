using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.M365.GraphHack.Lib.GameManager
{
    public class GameHelper
    {
        public static bool IsWinningMoveSet(List<int> moves)
        {
            if(moves.Contains(1) && moves.Contains(2) && moves.Contains(3))
            {
                return true;
            }
            if (moves.Contains(1) && moves.Contains(4) && moves.Contains(5))
            {
                return true;
            }
            if (moves.Contains(1) && moves.Contains(5) && moves.Contains(9))
            {
                return true;
            }
            if (moves.Contains(2) && moves.Contains(5) && moves.Contains(8))
            {
                return true;
            }
            if (moves.Contains(3) && moves.Contains(6) && moves.Contains(9))
            {
                return true;
            }
            if (moves.Contains(4) && moves.Contains(5) && moves.Contains(6))
            {
                return true;
            }
            if (moves.Contains(7) && moves.Contains(8) && moves.Contains(9))
            {
                return true;
            }
            if (moves.Contains(7) && moves.Contains(5) && moves.Contains(3))
            {
                return true;
            }

            return false;
        }
    }
}
