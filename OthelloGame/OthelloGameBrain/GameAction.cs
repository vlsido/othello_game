using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Db;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace OthelloGameBrain
{
    public class GameAction
    {
        // TODO: Get boards, check if valid move, place piece, flip pieces, check if game over, check if player can move, switch player, repeat
        public string Start(OthelloBrain brain, BoardSquareState[,] board, int axisX, int axisY, string winner, int whiteScore, 
            int blackScore, AppDbContext othelloDb)
        {
            var navigation = new Navigation();
            var validMove = new ValidMoves();
            BoardSquareState chosenByAi = new BoardSquareState();

           
            List<List<BoardSquareState>> linesOfSquares = new List<List<BoardSquareState>>();
            List<BoardSquareState> validForOpponent = new List<BoardSquareState>();

            var gameOver = false;
            

            board[axisX, axisY].IsSelected = true;


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
                    winner, blackScore, whiteScore, othelloDb);

                if (brain.OpponentType == EPlayerType.Ai)
                {
                    Thread.Sleep(1500);
                    (board, linesOfSquares) = validMove.CheckValidMoves(brain, board, linesOfSquares);
                    for (var x = 0; x < linesOfSquares.Count; x++)
                    {
                        for (var y = 0; y < linesOfSquares[x].Count; y++)
                        {
                            if (linesOfSquares[x][y].IsValid == true)
                            {
                                validForOpponent.Add(linesOfSquares[x][y]);
                            }
                           
                        }
                    }

                    var chosenFromList = _rnd.Next(0, validForOpponent.Count);
                    chosenByAi = validForOpponent[chosenFromList];
                    chosenByAi.PlayerColor = "White";
                    board[chosenByAi.X, chosenByAi.Y]
                        .IsPlaced = true;
                    board[chosenByAi.X, chosenByAi.Y]
                        .PlayerColor = "White";

                    for (var x = 0; x < linesOfSquares.Count; x++)
                    {
                        for (var y = 0; y < linesOfSquares[x].Count; y++)
                        {
                            if (linesOfSquares[x][y].X == chosenByAi.X && linesOfSquares[x][y].Y == chosenByAi.Y)
                            {
                                foreach (var item in linesOfSquares[x])
                                {
                                    if (item.IsValid)
                                    {
                                        board[item.X, item.Y].IsPlaced = true;
                                        board[item.X, item.Y].PlayerColor = "White";
                                    }

                                    if (item.PlayerColor == "Black")
                                    {
                                        board[item.X, item.Y].PlayerColor = "White";
                                    }
                                }

                            }
                        }
                    }

                    if (brain.CurrentPlayer == "White")
                    {
                        brain.CurrentPlayer = "Black";
                    }
                    else if (brain.CurrentPlayer == "Black")
                    {
                        brain.CurrentPlayer = "White";
                    }
                }

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
