﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloGameBrain
{
    public class Navigation
    {
        public (OthelloBrain, BoardSquareState[,]) Navigate(OthelloBrain brain, BoardSquareState[,] board, int axisX, int axisY)
        {
            var validMove = new ValidMoves();

            
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

                validMove.CheckValidMoves(brain, board);
                OthelloUI.DrawBoard(board);
                // Starting position of cursor

                // check if player can move
                if (brain.CurrentPlayer == "Black")
                {
                    Console.WriteLine("-\nBlack to move");
                }
                else if (brain.CurrentPlayer == "White")
                {
                    Console.WriteLine("-\nWhite to move");
                }

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
                            for (var x = 1; x < board.GetLength(0) - axisX; x++)
                            {
                                if (board[axisX + x, axisY].IsPlaced &&
                                    board[axisX + x, axisY].PlayerColor != playerColor)
                                {
                                    board[axisX + x, axisY].PlayerColor = playerColor;
                                    moveDone = true;
                                }
                                else
                                {
                                    break;
                                }
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
            return (brain, board);
        }
    }
}
