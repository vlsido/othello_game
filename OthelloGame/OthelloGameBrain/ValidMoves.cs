using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace OthelloGameBrain
{
    public class ValidMoves
    {
        public List<BoardSquareState[,]> Squares { get; set; }

        public ValidMoves()
        {
            Squares = new List<BoardSquareState[,]>();
        }

        public void AddSquare(BoardSquareState[,] square)
        {
            Squares.Add(square);
        }

        public BoardSquareState[,] CheckValidMoves(OthelloBrain brain, BoardSquareState[,] board)
        {
            // clean previous valid moves
            for (var x = 0; x < board.GetLength(0); x++)
            for (var y = 0; y < board.GetLength(1); y++)
                board[x, y].IsValid = false;
            
            var playerColor = "";

            var checkHorizontally = true;
            var checkFurtherRight = true;
            var checkFurtherLeft = true;
            var foundRight = false;
            var foundLeft = false;

            var checkVertically = true;
            var checkFurtherUp = true;
            var checkFurtherDown = true;
            var foundUp = false;
            var foundDown = false;

            var checkDiagonallySlashWise = true;
            var checkDiagonallyBackSlashWise = true;
            var checkFurtherUpRight = true;
            var checkFurtherUpLeft = true;
            var checkFurtherDownRight = true;
            var checkFurtherDownLeft = true;
            var foundUpRight = false;
            var foundUpLeft = false;
            var foundDownRight = false;
            var foundDownLeft = false;

            var squareRight = 0;
            var squareLeft = 0;
            var squareUp = 0;
            var squareDown = 0;
            var squareUpRight = 0;
            var squareUpLeft = 0;
            var squareDownRight = 0;
            var squareDownLeft = 0;

            if (brain.CurrentPlayer == "Black")
            {
                playerColor = "Black";
            }
            else if (brain.CurrentPlayer == "White")
            {
                playerColor = "White";
            }
            else
            {
                throw new BadPlayerException("Invalid player color", brain.CurrentPlayer);
            }

            // TODO: check valid moves
            for (var x = 0; x < board.GetLength(0); x++)
            for (var y = 0; y < board.GetLength(1); y++)
                if (board[x, y].PlayerColor is ("White" or "Black"))
                {
                    if (board[x, y].PlayerColor != playerColor)
                    {
                        for (var n = 0; n < board.GetLength(0); n++)
                        {
                            if (checkHorizontally)
                            {
                                
                                    for (var i = 0; i < board.GetLength(0); i++)
                                    {
                                        if (checkFurtherRight)
                                        {
                                            if (x + i < board.GetLength(0))
                                            { 
                                                if (board[x + i, y].PlayerColor != playerColor && board[x + i, y].IsPlaced)
                                                {
                                                }
                                                else if (board[x + i, y].PlayerColor == playerColor && board[x + i, y].IsPlaced)
                                                {
                                                    foundRight = true;
                                                    checkFurtherRight = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    squareRight = i;
                                                    checkFurtherRight = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                
                                    for (var i = 0; i < board.GetLength(0); i++)
                                    {
                                        if (checkFurtherLeft)
                                        {
                                            if ((x - i) >= 0)
                                            { 
                                                if (board[x - i, y].PlayerColor != playerColor && board[x - i, y].IsPlaced)
                                                {
                                                } 
                                                else if (board[x - i, y].PlayerColor == playerColor && board[x - i, y].IsPlaced)
                                                {
                                                    foundLeft = true;
                                                    checkFurtherLeft = false;
                                                }
                                                else
                                                {
                                                    squareLeft = i;
                                                    checkFurtherLeft = false;
                                                }
                                            }
                                        }
                                    }

                                    if (foundRight || foundLeft)
                                    {
                                        if (board[x + squareRight, y].IsPlaced == false)
                                        {
                                            board[x + squareRight, y].IsValid = true;
                                        }

                                        if (board[x - squareLeft, y].IsPlaced == false)
                                        {
                                            board[x - squareLeft, y].IsValid = true;
                                        }
                                    }
                            }

                            if (checkVertically)
                            {
                                    for (var i = 0; i < board.GetLength(1); i++)
                                    {
                                        if (checkFurtherUp)
                                        {
                                            if (y + i < board.GetLength(1))
                                            {
                                                if (board[x, y + i].PlayerColor != playerColor && board[x, y + i].IsPlaced)
                                                {
                                                }
                                                else if (board[x, y + i].PlayerColor == playerColor && board[x, y + i].IsPlaced)
                                                {
                                                    foundUp = true;
                                                    checkFurtherUp = false;
                                                }
                                                else
                                                {
                                                    squareUp = i;
                                                    checkFurtherUp = false; ;
                                                }
                                            }
                                        }
                                    }

                                    for (var i = 0; i < board.GetLength(1); i++)
                                    {
                                        if (checkFurtherDown)
                                        {
                                            if ((y - i) >= 0)
                                            {
                                                if (board[x, y - i].PlayerColor != playerColor && board[x, y - i].IsPlaced)
                                                {
                                                }
                                                else if (board[x, y - i].PlayerColor == playerColor && board[x, y - i].IsPlaced)
                                                {
                                                    foundDown = true;
                                                    checkFurtherDown = false;
                                                }
                                                else
                                                {
                                                    squareDown = i;
                                                    checkFurtherDown = false;
                                                }
                                            }
                                        }
                                    }
                                    if (foundUp || foundDown)
                                    {
                                        if (board[x, y + squareUp].IsPlaced == false)
                                        {
                                            board[x, y + squareUp].IsValid = true;
                                        }

                                        if (board[x, y - squareDown].IsPlaced == false)
                                        {
                                            board[x, y - squareDown].IsValid = true;
                                        }
                                    }
                            }

                            if (checkDiagonallySlashWise)
                            {
                                    for (var i = 0; i < board.GetLength(0); i++)
                                    {
                                        if (checkFurtherUpRight)
                                        {
                                            if (x + i < board.GetLength(0) && y + i < board.GetLength(1))
                                            {
                                                if (board[x + i, y + i].PlayerColor != playerColor && board[x + i, y + i].IsPlaced)
                                                {
                                                }
                                                else if (board[x + i, y + i].PlayerColor == playerColor && board[x + i, y + i].IsPlaced)
                                                {
                                                    foundUpRight = true;
                                                    checkFurtherUpRight = false;
                                                }
                                                else
                                                {
                                                    squareUpRight = i;
                                                    checkFurtherUpRight = false;
                                                }
                                            }
                                        }
                                    }

                                    for (var i = 0; i < board.GetLength(0); i++)
                                    {
                                        if (checkFurtherDownLeft)
                                        {
                                            if ((x - i) >= 0 && (y - i) >= 0)
                                            {
                                                if (board[x - i, y - i].PlayerColor != playerColor && board[x - i, y - i].IsPlaced)
                                                {
                                                }
                                                else if (board[x - i, y - i].PlayerColor == playerColor && board[x - i, y - i].IsPlaced)
                                                {
                                                    foundDownLeft = true;
                                                    checkFurtherDownLeft = false;
                                                }
                                                else
                                                {
                                                    squareDownLeft = i;
                                                    checkFurtherDownLeft = false;
                                                }
                                            }
                                        }
                                    }
                                    if (foundUpRight || foundDownLeft)
                                    {
                                        if (board[x + squareUpRight, y + squareUpRight].IsPlaced == false)
                                        {
                                            board[x + squareUpRight, y + squareUpRight].IsValid = true;
                                        }

                                        if (board[x - squareDownLeft, y - squareDownLeft].IsPlaced == false)
                                        {
                                            board[x - squareDownLeft, y - squareDown].IsValid = true;
                                        }
                                    }
                            }

                            if (checkDiagonallyBackSlashWise)
                            {
                                    for (var i = 0; i < board.GetLength(0); i++)
                                    {
                                        if (checkFurtherUpLeft)
                                        {
                                            if ((x - i) >= 0 && y + i < board.GetLength(1))
                                            {
                                                if (board[x - i, y + i].PlayerColor != playerColor && board[x - i, y + i].IsPlaced)
                                                {
                                                }
                                                else if (board[x - i, y + i].PlayerColor == playerColor && board[x - i, y + i].IsPlaced)
                                                {
                                                    foundUpLeft = true;
                                                    checkFurtherUpLeft = false;
                                                }
                                                else
                                                {
                                                    squareUpLeft = i;
                                                    checkFurtherUpLeft = false;
                                                }
                                            }
                                        }
                                    }

                                    for (var i = 0; i < board.GetLength(0); i++)
                                    {
                                        if (checkFurtherDownRight)
                                        {
                                            if ((x + i) < board.GetLength(0) && (y - i) >= 0)
                                            {
                                                if (board[x + i, y - i].PlayerColor != playerColor && board[x + i, y - i].IsPlaced)
                                                {
                                                }
                                                else if (board[x + i, y - i].PlayerColor == playerColor && board[x + i, y - i].IsPlaced)
                                                {
                                                    foundDownRight = true;
                                                    checkFurtherDownRight = false;
                                                }
                                                else
                                                {
                                                    squareDownRight = i;
                                                    checkFurtherDownRight = false;
                                                }
                                            }
                                        }
                                    }
                                    if (foundUpLeft || foundDownRight)
                                    {
                                        if (board[x - squareUpLeft, y + squareUpLeft].IsPlaced == false)
                                        {
                                            board[x + squareUpLeft, y - squareUpLeft].IsValid = true;
                                        }

                                        if (board[x - squareDownRight, y + squareDownRight].IsPlaced == false)
                                        {
                                            board[x + squareDownRight, y - squareDownRight].IsValid = true;
                                        }
                                    }
                            }
                            


                        }

                        checkFurtherRight = true;
                        checkFurtherLeft = true;
                        checkFurtherUp = true;
                        checkFurtherDown = true;
                        checkFurtherDownLeft = true;
                        checkFurtherDownRight = true;
                        checkFurtherUpRight = true;
                        checkFurtherUpLeft = true;

                    }
                }

            return board;
        }
    }
}
