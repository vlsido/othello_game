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
        public string PlayerColor { get; set; }
        public bool IsSelected { get; set; }
        public bool IsValid { get; set; }

        public override string ToString()
        {
            switch (IsValid)
            {
                case true:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[_]");
                    Console.ResetColor();
                    return "";
            }

            switch (IsPlaced)
            {
                case false:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("[_]");
                    Console.ResetColor();
                    return "";
            }
            switch (IsPlaced, PlayerColor, IsSelected)
            {
                case (true, "White", false):
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("[W]");
                    Console.ResetColor();
                    return "";
                case (true, "Black", false):
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("[B]");
                    Console.ResetColor();
                    return "";
                case (true, "White", true):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[W]");
                    Console.ResetColor();
                    return "";
                case (true, "Black", true):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[B]");
                    Console.ResetColor();
                    return "";
            }
            return "";
        }
    }
    }
    
