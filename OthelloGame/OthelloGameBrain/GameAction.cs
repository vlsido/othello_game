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

            int axisX = 0;
            int axisY = 0;
            do
            {
               
                    // draw board
                Console.Clear();
                OthelloUI.DrawBoard(board);
                // Starting position of cursor

                // check if player can move
                if (brain.CurrentPlayer == "Black")
                {
                    Console.WriteLine("-\nBlack to move");
                }
                var moveDone = false;
                var keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        board[axisX, axisY].IsSelected = false;
                        if (axisY - 1 >= 0) axisY -= 1;
                        board[axisX, axisY].IsSelected = true;
                        break;
                    case ConsoleKey.DownArrow:
                        if (axisY + 1 > brain.BoardSizeVertical - 1)
                        {
                            break;
                        }
                        else
                        {
                            board[axisX, axisY].IsSelected = false;
                            axisY += 1;
                            board[axisX, axisY].IsSelected = true;
                        }
                        
                        break;
                    case ConsoleKey.RightArrow:
                        if (axisX + 1 > brain.BoardSizeHorizontal - 1)
                        {
                            break;
                        }
                        else
                        {
                            board[axisX, axisY].IsSelected = false;
                            axisX += 1;
                            board[axisX, axisY].IsSelected = true;
                        }

                        break;
                    case ConsoleKey.LeftArrow:
                        board[axisX, axisY].IsSelected = false;
                        if (axisX - 1 >= 0) axisX -= 1;
                        board[axisX, axisY].IsSelected = true;
                        break;
                }
                // if player can move
                // player moves
                // check if valid move
                // if valid move
                // place piece
                // flip pieces
                // check if game over
                // if game over
                // gameOver = true
                // else
                // switch player
                // check if player can move
                // if player can move
                // player moves
                // check if valid move
                // if valid move
                // place piece
                // flip pieces
                // check if game over
                // if game over
                // gameOver = true
                // else
                // switch player

            } while (gameOver == false);
            return "";
        }
    }
}
