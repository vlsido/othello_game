﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloGameBrain
{
    public class Options
    {
        // todo: default settings (piece position)
        public BoardSquareState[,] DefaultOptions(BoardSquareState[,] board)
        {
            board[3, 3].IsPlaced = true;
            board[3, 3].IsWhite = true;
            board[4, 4].IsPlaced = true;
            board[4, 4].IsWhite = true;
            board[3, 4].IsPlaced = true;
            board[3, 4].IsWhite = false;
            board[4, 3].IsPlaced = true;
            board[4, 3].IsWhite = false;
            
            return board;
        }
     
        
    }
}