using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloGameBrain
{
    public struct BoardSquareState
    {
        public bool IsPlaced { get; set; }
        public bool IsWhite { get; set; }

        public override string ToString()
        {
            switch (IsPlaced, IsWhite)
            {
                case (true, true):
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("[W]");
                    Console.ResetColor();
                    return "";
                case (true, false):
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("[B]");
                    Console.ResetColor();
                    return "";
                case (false, true):
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[ ]");
                    Console.ResetColor();
                    return "";
                case (false, false):
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[ ]");
                    Console.ResetColor();
                    return "";
            }
        }
    }
    }
    
