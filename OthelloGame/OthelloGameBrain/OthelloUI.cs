using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloGameBrain
{
    public static class OthelloUI
    {
        public static void DrawBoard(BoardSquareState[,] board)
        {
            for (var y = 0; y < board.GetLength(1); y++)
            {
                Console.Write(y + "  ");
                for (var x = 0; x < board.GetLength(0); x++) Console.Write(board[x, y]);
                Console.WriteLine();
            }
        }
    }
}
