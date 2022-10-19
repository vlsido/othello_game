using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OthelloGameBrain
{
    public struct BoardSquareState
    {
        public bool IsPlaced { get; set; }
        public string PlayerColor { get; set; }
        public bool IsSelected { get; set; }
        public bool IsValid { get; set; }
        public bool IsFileNotation { get; set; }
        public bool ToFlip { get; set; }
        public int X { get; set; }
        public int Y { get; set; }


        public override string ToString()
        {
            switch (IsValid, PlayerColor)
            {
                case (true, "Black"):
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("[B]");
                    Console.ResetColor();
                    return "";
                case (true, "White"):
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("[W]");
                    Console.ResetColor();
                    return "";
            }

            switch (IsValid, IsPlaced, IsSelected)
            {
                case (true, false, false):
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("[_]");
                    Console.ResetColor();
                    return "";
                case (true, false, true):
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("[*]");
                    Console.ResetColor();
                    return "";
            }


            switch (IsPlaced, IsSelected)
            {
                case (false, false):
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("[_]");
                    Console.ResetColor();
                    return "";
                case (false, true):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[*]");
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
    
