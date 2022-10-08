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
        public string Start(OthelloBrain brain, BoardSquareState[,] board, BoardSize boardSize)
        {
            const bool gameOver = false;
            brain.GetBoard();
            board[0, 0].IsSelected = true;

            // if default settings
            // { }

            // play game until gameOver == true

            
            var _rnd = new Random();
            
            // choose game vs comp or vs real player

            if (_rnd.Next(0, 1) == 0)
            {
                Console.WriteLine("You play as Black");
            }
            else
            {
                Console.WriteLine("You play as White");
            }

            // Throw goofy exception

            //brain.CurrentPlayer = "Intruder";

            if (brain.CurrentPlayer is not ("Black" or "White"))
            {
                throw new BadPlayerException("Don't mess with this code >:|", brain.CurrentPlayer);
            }
            
            // TODO: Check valid moves (and highlight them)
            

            return "";
        }
    }
}
