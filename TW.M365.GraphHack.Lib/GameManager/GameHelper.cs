using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.M365.GraphHack.Lib.GameManager
{
    public class GameHelper
    {
        public static string IsWinningMoveSet(List<int> moves)
        {
            if (moves.Contains(1) && moves.Contains(2) && moves.Contains(3))
            {
                return "1,2,3";
            }
            if (moves.Contains(1) && moves.Contains(4) && moves.Contains(7))
            {
                return "1,4,5";
            }
            if (moves.Contains(1) && moves.Contains(5) && moves.Contains(9))
            {
                return "1,5,9";
            }
            if (moves.Contains(2) && moves.Contains(5) && moves.Contains(8))
            {
                return "2,5,8";
            }
            if (moves.Contains(3) && moves.Contains(6) && moves.Contains(9))
            {
                return "3,6,9";
            }
            if (moves.Contains(4) && moves.Contains(5) && moves.Contains(6))
            {
                return "4,5,6";
            }
            if (moves.Contains(7) && moves.Contains(8) && moves.Contains(9))
            {
                return "7,8,9";
            }
            if (moves.Contains(7) && moves.Contains(5) && moves.Contains(3))
            {
                return "7,5,3";
            }

            return "";
        }
    }
}
