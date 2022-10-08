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
        public bool IsSelected { get; set; }

        public override string ToString()
        {
            switch (IsPlaced, IsWhite, IsSelected)
            {
                case (true, true, false):
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("[W]");
                    Console.ResetColor();
                    return "";
                case (true, false, false):
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("[B]");
                    Console.ResetColor();
                    return "";
                case (false, true, false):
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("[_]");
                    Console.ResetColor();
                    return "";
                case (false, false, false):
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("[_]");
                    Console.ResetColor();
                    return "";
                case (true, true, true):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[W]");
                    Console.ResetColor();
                    return "";
                case (true, false, true):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[B]");
                    Console.ResetColor();
                    return "";
                case (false, true, true):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[_]");
                    Console.ResetColor();
                    return "";
                case (false, false, true):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[_]");
                    
                    return "";
            }
        }
    }
    }
    
