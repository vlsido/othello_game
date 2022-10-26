using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloGameBrain
{
    public class Options
    {
        public BoardSquareState[,] DefaultOptions(BoardSquareState[,] board)
        {
            
            board[3, 3].IsPlaced = true;
            board[3, 3].PlayerColor = "White";
            board[4, 4].IsPlaced = true;
            board[4, 4].PlayerColor = "White";
            board[3, 4].IsPlaced = true;
            board[3, 4].PlayerColor = "Black";
            board[4, 3].IsPlaced = true;
            board[4, 3].PlayerColor = "Black";
            
            return board;
        }
     
        
    }
}
