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
        public string Start(OthelloBrain brain, BoardSquareState[,] board, BoardSize boardSize, int axisX, int axisY, string winner, int whiteScore, int blackScore)
        {
            var navigation = new Navigation();

           
            List<List<BoardSquareState>> linesOfSquares = new List<List<BoardSquareState>>();

            var gameOver = false;

            brain.GetBoard();
         
            board[axisX, axisY].IsSelected = true;
            
            
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

            

            do
            {
                (brain, board, axisX, axisY, winner) = navigation.Navigate(brain, board, linesOfSquares, axisX, axisY,
                    winner, blackScore, whiteScore);
                if (winner != "null")
                {
                    gameOver = true;
                }
            } while (gameOver == false);

            Console.Clear();
            OthelloUI.DrawBoard(board);

            if (winner == "Black")
            {
                Console.WriteLine("Black won!");
            }
            else if (winner == "White")
            {
                Console.WriteLine("White won!");
            } else if (winner == "Tie")
            {
                Console.WriteLine("Game ended in tie!");
            }

            Thread.Sleep(1000000);
            return "";
        }
    }
}
