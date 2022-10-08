using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloGameBrain
{
    public class Navigation
    {
        (OthelloBrain, BoardSquareState[,]) Navigate(OthelloBrain brain, BoardSquareState[,] board)
        {
            int axisX = 0;
            int axisY = 0;
            var moveDone = false;
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
                    case ConsoleKey.Enter:
                        if (board[axisX, axisY].IsPlaced == false)
                        {
                            if (brain.CurrentPlayer == "Black")
                            {

                            }
                        }
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

            } while (moveDone == false);
            return (brain, board);
        }
    }
}
