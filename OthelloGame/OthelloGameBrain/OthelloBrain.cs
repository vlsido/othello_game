using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloGameBrain
{
    public class OthelloBrain
    {
        public string CurrentPlayer = "Black";
        public GameBoard GameBoard;

        private readonly Random _rnd = new();

        public int BoardSizeHorizontal { get; set; } = 8;
        public int BoardSizeVertical { get; set; } = 8;

        public OthelloBrain(int boardSizeHorizontal, int boardSizeVertical)
        {
            GameBoard = new GameBoard
            {
                Board = new BoardSquareState[boardSizeHorizontal, boardSizeVertical]
            };
        }

        public BoardSquareState[,] GetBoard()
        {
            return CreateBoard(GameBoard.Board);
        }

        private BoardSquareState[,] CreateBoard(BoardSquareState[,] board)
        {
            var res = new BoardSquareState[board.GetLength(0), board.GetLength(1)];
            for (var x = 0; x < board.GetLength(0); x++)
            for (var y = 0; y < board.GetLength(1); y++)
            {
                board[x, 0].IsFileNotation = true;
                res[x, y] = board[x, y];
            }
                
            return res;
        }


    }
}
