using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OthelloGameBrain.GameDTO;

namespace OthelloGameBrain
{
    public class GameDTO
    {

        public GameBoardDTO GameBoard { get; set; }
        public string CurrentPlayer { get; set; } = string.Empty;
        public int BoardSizeHorizontal { get; set; }
        public int BoardSizeVertical { get; set; }

        public GameDTO()
        {
            GameBoard = new GameBoardDTO(new List<List<BoardSquareState>>());
        }

        public class GameBoardDTO
        {
            public GameBoardDTO(List<List<BoardSquareState>> board)
            {
                Board = board;
            }

            public List<List<BoardSquareState>> Board { get; set; } = null!;
        }


    }
}
