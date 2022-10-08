using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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

        BoardSquareState[,] CheckValidMoves(OthelloBrain brain, BoardSquareState[,] board)
        {
            var playerColor = "";
            if (brain.CurrentPlayer == "Black")
            {
                playerColor = "Black";
            } else if (brain.CurrentPlayer == "White")
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
                    if (board[x + 1, y].PlayerColor == playerColor)
                    {
                        
                    }
            return board;
        }
    }
}
