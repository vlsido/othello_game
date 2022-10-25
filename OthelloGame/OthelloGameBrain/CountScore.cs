using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloGameBrain
{
    public class CountScore
    {
        public (int, int, string) Score(BoardSquareState[,] board, int blackScore, int whiteScore, string winner, int leftSquares)
        {
            blackScore = 0;
            whiteScore = 0;
            for (var x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y].PlayerColor == "Black")
                    {
                        blackScore += 1;
                    }
                    else if (board[x, y].PlayerColor == "White")
                    {
                        whiteScore += 1;
                    }

                    if (!board[x, y].IsPlaced)
                    {
                        leftSquares += 1;
                    }
                }
            }

            if (leftSquares == 0 && blackScore > whiteScore)
            {
                winner = "Black";
            } else if (leftSquares == 0 && blackScore < whiteScore)
            {
                winner = "White";
            } else if (leftSquares == 0 && blackScore == whiteScore)
            {
                winner = "Tie";
            }

            return (whiteScore, blackScore, winner);
        }
    }
}
