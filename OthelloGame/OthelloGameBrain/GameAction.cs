using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloGameBrain
{
    public class GameAction
    {
        // TODO: Get boards, check if valid move, place piece, flip pieces, check if game over, check if player can move, switch player, repeat
        public string Start(OthelloBrain brain, BoardSquareState[,] playerBlack, BoardSquareState[,] playerWhite,
            BoardSize boardSize)
        {
            brain.GetBoard(0);
            brain.GetBoard(1);
            OthelloUI.DrawBoard(playerBlack);
            // sleep for 5 seconds
            System.Threading.Thread.Sleep(5000);
            return "";
        }
    }
}
