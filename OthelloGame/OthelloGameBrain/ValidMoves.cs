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
    public class ValidMoves : ICloneable
    {
        public List<BoardSquareState> Squares { get; set; }

        public object Clone() { return this.MemberwiseClone(); }
        public ValidMoves()
        {
            Squares = new List<BoardSquareState>();
        }

        public void AddSquare(BoardSquareState square)
        {
            Squares.Add(square);
        }

        public (BoardSquareState[,], List<List<BoardSquareState>>) CheckValidMoves(OthelloBrain brain, BoardSquareState[,] board, List<List<BoardSquareState>> linesOfSquares)
        {
            // clean previous valid moves
            for (var x = 0; x < board.GetLength(0); x++)
            for (var y = 0; y < board.GetLength(1); y++)
                board[x, y].IsValid = false;
            
            var playerColor = "";

            var coordinates = new Coordinates();

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
            var placedRight = 0;
            var placedLeft = 0;

            var squareUp = 0;
            var squareDown = 0;
            var placedUp = 0;
            var placedDown = 0;

            var squareUpRight = 0;
            var squareUpLeft = 0;
            var placedUpRight = 0;
            var placedUpLeft = 0;

            var squareDownRight = 0;
            var squareDownLeft = 0;
            var placedDownRight = 0;
            var placedDownLeft = 0;

            // Square dobavit v lineSquares

            // TODO: добавить линии квадратов от ВалидМува до ФаундСквер, записать их все лист
            // if (linesOfSquares[i][n].X == x && .Y == y)
            // linesOfSquares[i] place not placed and flip opponent pieces

            List<BoardSquareState> squares = new List<BoardSquareState>();
            linesOfSquares.Clear();

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
                                                    placedRight = i;
                                                    checkFurtherRight = false;
                                                }
                                                else if (!board[x + i, y].IsPlaced)
                                                {
                                                    squareRight = i;
                                                    checkFurtherRight = false;
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
                                                    placedLeft = i;
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

                                    

                                    
                            }

                            if (checkVertically)
                            {
                                for (var i = 0; i < board.GetLength(1); i++)
                                {
                                    if (checkFurtherDown)
                                    {
                                        if (y + i < board.GetLength(1))
                                        {
                                            if (board[x, y + i].PlayerColor != playerColor && board[x, y + i].IsPlaced)
                                            {
                                            }
                                            else if (board[x, y + i].PlayerColor == playerColor && board[x, y + i].IsPlaced)
                                            {
                                                foundDown = true;
                                                placedDown = i;
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

                                for (var i = 0; i < board.GetLength(1); i++)
                                {
                                    if (checkFurtherUp)
                                    {
                                        if ((y - i) >= 0)
                                        {
                                            if (board[x, y - i].PlayerColor != playerColor && board[x, y - i].IsPlaced)
                                            {
                                            }
                                            else if (board[x, y - i].PlayerColor == playerColor && board[x, y - i].IsPlaced)
                                            {
                                                foundUp = true;
                                                placedUp = i;
                                                checkFurtherUp = false;
                                            }
                                            else
                                            {
                                                squareUp = i;
                                                checkFurtherUp = false;
                                            }
                                        }
                                    }
                                }
                                

                                
                            }

                            if (checkDiagonallySlashWise)
                            {
                                for (var i = 0; i < board.GetLength(0); i++)
                                {
                                    if (checkFurtherUpRight)
                                    {
                                        if (x + i < board.GetLength(0) && y - i >= 0)
                                        {
                                            if (board[x + i, y - i].PlayerColor != playerColor && board[x + i, y - i].IsPlaced)
                                            {
                                            }
                                            else if (board[x + i, y - i].PlayerColor == playerColor && board[x + i, y - i].IsPlaced)
                                            {
                                                foundUpRight = true;
                                                placedUpRight = i;
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
                                        if ((x - i) >= 0 && (y + i) < board.GetLength(1))
                                        {
                                            if (board[x - i, y + i].PlayerColor != playerColor && board[x - i, y + i].IsPlaced)
                                            {
                                            }
                                            else if (board[x - i, y + i].PlayerColor == playerColor && board[x - i, y + i].IsPlaced)
                                            {
                                                foundDownLeft = true;
                                                placedDownLeft = i;
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
                                
                                
                            }

                            if (checkDiagonallyBackSlashWise)
                            {
                                for (var i = 0; i < board.GetLength(0); i++)
                                {
                                    if (checkFurtherUpLeft)
                                    {
                                        if (x - i >= 0 && y - i >= 0)
                                        {
                                            if (board[x - i, y - i].PlayerColor != playerColor && board[x - i, y - i].IsPlaced)
                                            {
                                            }
                                            else if (board[x - i, y - i].PlayerColor == playerColor && board[x - i, y - i].IsPlaced)
                                            {
                                                foundUpLeft = true;
                                                placedUpLeft = i;
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
                                        if ((x + i) < board.GetLength(0) && (y + i) < board.GetLength(1))
                                        {
                                            if (board[x + i, y + i].PlayerColor != playerColor && board[x + i, y + i].IsPlaced)
                                            {
                                            }
                                            else if (board[x + i, y + i].PlayerColor == playerColor && board[x + i, y + i].IsPlaced)
                                            {
                                                foundDownRight = true;
                                                checkFurtherDownRight = false;
                                                placedDownRight = i;
                                            }
                                            else
                                            {
                                                squareDownRight = i;
                                                checkFurtherDownRight = false;
                                            }
                                        }
                                    }
                                }
                                
                                
                            }

                            checkFurtherRight = true;
                            checkFurtherLeft = true;
                            

                            checkFurtherUp = true;
                            checkFurtherDown = true;

                            checkFurtherDownLeft = true;
                            checkFurtherUpRight = true;

                            checkFurtherUpLeft = true;
                            checkFurtherDownRight = true;

                        }





                            if (foundRight || foundLeft)
                            {
                                if (board[x + squareRight, y].IsPlaced == false)
                                {
                                    board[x + squareRight, y].IsValid = true;
                                }

                                if (foundRight)
                                {
                                    for (var i = squareLeft; i >= 0; i--)
                                    {
                                        board[x - i, y].X = x - i;
                                        board[x - i, y].Y = y;
                                        squares.Add(board[x - i, y]);
                                    }

                                    for (var i = 1; i <= placedRight; i++)
                                    {
                                        board[x + i, y].X = x + i;
                                        board[x + i, y].Y = y;
                                        squares.Add(board[x + i, y]);
                                    }

                                    linesOfSquares.Add(new List<BoardSquareState>(squares));
                                    squares.Clear();
                                    foundRight = false;
                                }

                                if (board[x - squareLeft, y].IsPlaced == false)
                                {
                                    board[x - squareLeft, y].IsValid = true;
                                }

                                if (foundLeft)
                                {
                                    for (var i = placedLeft; i >= 0; i--)
                                    {
                                        board[x - i, y].X = x - i;
                                        board[x - i, y].Y = y;
                                        squares.Add(board[x - i, y]);
                                    }

                                    for (var i = 1; i <= squareRight; i++)
                                    {
                                        board[x + i, y].X = x + i;
                                        board[x + i, y].Y = y;
                                        squares.Add(board[x + i, y]);
                                    }
                                    linesOfSquares.Add(new List<BoardSquareState>(squares));
                                    squares.Clear();
                                    foundLeft = false;
                                }

                            }

                            if (foundUp || foundDown)
                            {
                                if (board[x, y - squareUp].IsPlaced == false)
                                {
                                    board[x, y - squareUp].IsValid = true;
                                }

                                if (foundUp)
                                {
                                    for (var i = placedUp; i >= 0; i--)
                                    {
                                        board[x, y - i].X = x;
                                        board[x, y - i].Y = y - i;
                                        squares.Add(board[x, y - i]);
                                    }

                                    for (var i = 1; i <= squareDown; i++)
                                    {
                                        board[x, y + i].X = x;
                                        board[x, y + i].Y = y + i;
                                        squares.Add(board[x, y + i]);
                                    }

                                    linesOfSquares.Add(new List<BoardSquareState>(squares));
                                    squares.Clear();
                                    foundUp = false;
                                }

                                if (board[x, y + squareDown].IsPlaced == false)
                                {
                                    board[x, y + squareDown].IsValid = true;
                                }

                                if (foundDown)
                                {
                                    for (var i = squareUp; i >= 0; i--)
                                    {
                                        board[x, y - i].X = x;
                                        board[x, y - i].Y = y - i;
                                        squares.Add(board[x, y - i]);
                                    }

                                    for (var i = 1; i <= placedDown; i++)
                                    {
                                        board[x, y + i].X = x;
                                        board[x, y + i].Y = y + i;
                                        squares.Add(board[x, y + i]);
                                    }

                                    linesOfSquares.Add(new List<BoardSquareState>(squares));
                                    squares.Clear();
                                    foundDown = false;
                                }
                            }

                            if (foundUpRight || foundDownLeft)
                            {
                                if (board[x + squareUpRight, y - squareUpRight].IsPlaced == false)
                                {
                                    board[x + squareUpRight, y - squareUpRight].IsValid = true;
                                }

                                if (foundUpRight)
                                {
                                    for (var i = squareDownLeft; i >= 0; i--)
                                    {
                                        board[x - i, y + i].X = x - i;
                                        board[x - i, y + i].Y = y + i;
                                        squares.Add(board[x - i, y + i]);
                                    }

                                    for (var i = 1; i <= placedUpRight; i++)
                                    {
                                        board[x + i, y - i].X = x + i;
                                        board[x + i, y - i].Y = y - i;
                                        squares.Add(board[x + i, y - i]);
                                    }

                                    linesOfSquares.Add(new List<BoardSquareState>(squares));
                                    squares.Clear();
                                    foundUpRight = false;
                                }

                                if (board[x - squareDownLeft, y + squareDownLeft].IsPlaced == false)
                                {
                                    board[x - squareDownLeft, y + squareDownLeft].IsValid = true;
                                }

                                if (foundDownLeft)
                                {
                                    for (var i = placedDownLeft; i >= 0; i--)
                                    {
                                        board[x - i, y + i].X = x - i;
                                        board[x - i, y + i].Y = y + i;
                                        squares.Add(board[x - i, y + i]);
                                    }

                                    for (var i = 1; i <= squareUpRight; i++)
                                    {
                                        board[x + i, y - i].X = x + i;
                                        board[x + i, y - i].Y = y - i;
                                        squares.Add(board[x + i, y - i]);
                                    }

                                    linesOfSquares.Add(new List<BoardSquareState>(squares));
                                    squares.Clear();
                                    foundDownLeft = false;
                                }
                            }

                            if (foundUpLeft || foundDownRight)
                            {
                                if (board[x - squareUpLeft, y - squareUpLeft].IsPlaced == false)
                                {
                                    board[x - squareUpLeft, y - squareUpLeft].IsValid = true;
                                }

                                if (foundUpLeft)
                                {
                                    for (var i = placedUpLeft; i >= 0; i--)
                                    {
                                        board[x - i, y - i].X = x - i;
                                        board[x - i, y - i].Y = y - i;
                                        squares.Add(board[x - i, y - i]);
                                    }

                                    for (var i = 1; i <= squareDownRight; i++)
                                    {
                                        board[x + i, y + i].X = x + i;
                                        board[x + i, y + i].Y = y + i;
                                        squares.Add(board[x + i, y + i]);
                                    }

                                    linesOfSquares.Add(new List<BoardSquareState>(squares));
                                    squares.Clear();
                                    foundUpLeft = false;
                                }

                                if (board[x + squareDownRight, y + squareDownRight].IsPlaced == false)
                                {
                                    board[x + squareDownRight, y + squareDownRight].IsValid = true;
                                }

                                if (foundDownRight)
                                {
                                    for (var i = squareUpLeft; i >= 0; i--)
                                    {
                                        board[x - i, y - i].X = x - i;
                                        board[x - i, y - i].Y = y - i;
                                        squares.Add(board[x - i, y - i]);
                                    }

                                    for (var i = 1; i <= placedDownRight; i++)
                                    {
                                        board[x + i, y + i].X = x + i;
                                        board[x + i, y + i].Y = y + i;
                                        squares.Add(board[x + i, y + i]);
                                    }

                                    linesOfSquares.Add(new List<BoardSquareState>(squares));
                                    squares.Clear();
                                    foundDownRight = false;
                                }
                            }

                            squareRight = 0;
                            squareLeft = 0;
                            squareUp = 0;
                            squareDown = 0;
                            squareUpRight = 0;
                            squareUpLeft = 0;
                            squareDownRight = 0;
                            squareDownLeft = 0;
                    }
                }
            
            return (board, linesOfSquares);
        }
    }
}
