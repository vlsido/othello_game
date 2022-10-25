using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloGameBrain
{
    public class Navigation
    {
        public (OthelloBrain, BoardSquareState[,], int, int, string) Navigate(OthelloBrain brain, BoardSquareState[,] board, 
            List<List<BoardSquareState>> linesOfSquares, int axisX, int axisY, string winner, int blackScore, int whiteScore)
        {
            var validMove = new ValidMoves();
            var validCount = 0;
            var changed = false;

            // TODO: если все плейсед, то чек счёт

            // First check
            validMove.CheckValidMoves(brain, board, linesOfSquares);
            for (var x = 0; x < board.GetLength(0); x++)
            {
                for (var y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y].IsValid)
                    {
                        validCount += 1;
                    }
                }
            }

            

            

            int leftSquares = 0;
            var score = new CountScore();

            

            var moveDone = false;
            do
            {

                // draw board
                Console.Clear();
                char[] notation = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                for (var l = 0; l < board.GetLength(0); l++)
                {
                    if (l == 0)
                        Console.Write("   ");
                    Console.Write(" " + notation[l] + " ");
                }
                Console.WriteLine("");

               
                OthelloUI.DrawBoard(board);

                

                // check if player can move
                if (brain.CurrentPlayer == "Black")
                {
                    Console.WriteLine("-\nBlack to move");
                }
                else if (brain.CurrentPlayer == "White")
                {
                    Console.WriteLine("-\nWhite to move");
                }
                (whiteScore, blackScore, winner) = score.Score(board, whiteScore, blackScore, winner, leftSquares);

                Console.WriteLine($"Black score: {blackScore}");
                Console.WriteLine($"White score: {whiteScore}");



                var playerColor = brain.CurrentPlayer;

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
                        if (board[axisX, axisY].IsValid)
                        {
                            board[axisX, axisY].IsPlaced = true;
                            board[axisX, axisY].PlayerColor = playerColor;

                            for (var i = 0; i < linesOfSquares.Count; i++)
                            {
                                for (var j = 0; j < linesOfSquares[i].Count; j++)
                                {
                                    if (linesOfSquares[i][j].X == axisX && linesOfSquares[i][j].Y == axisY)
                                    {
                                        foreach (var item in linesOfSquares[i])
                                        {
                                            if (item.IsValid)
                                            {
                                                board[item.X, item.Y].IsPlaced = true;
                                                board[item.X, item.Y].PlayerColor = playerColor;
                                            }

                                            if (item.PlayerColor != playerColor)
                                            {
                                                board[item.X, item.Y].PlayerColor = playerColor;
                                            }
                                        }
                                        
                                    }
                                }
                            }

                            moveDone = true;
                        }

                        
                        break;
                    case ConsoleKey.P:
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

                if (moveDone)
                {
                    if (brain.CurrentPlayer == "Black")
                    {
                        brain.CurrentPlayer = "White";
                    }
                    else if (brain.CurrentPlayer == "White")
                    {
                        brain.CurrentPlayer = "Black";
                    }
                }
                
            } while (moveDone == false);

            return (brain, board, axisX, axisY, winner);
        }
    }
}
