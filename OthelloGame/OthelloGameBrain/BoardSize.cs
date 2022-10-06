using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloGameBrain
{
    public class BoardSize
    {
        public OthelloBrain BSize(OthelloBrain boardSize)
        {
            Console.WriteLine("Horizontal size of the board: ");
            var xSize = Console.ReadLine();
            Console.WriteLine("Vertical size of the board: ");
            var ySize = Console.ReadLine();
            int.TryParse(xSize, out var xSizeConverted);
            int.TryParse(ySize, out var ySizeConverted);

            boardSize.BoardSizeHorizontal = xSizeConverted;
            boardSize.BoardSizeVertical = ySizeConverted;

            return boardSize;

        }
    }
}
