using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuSystem;
using OthelloGameBrain;

namespace OthelloGameBrain
{
    public class GameEngine
    {
        public string Menu()
        {

            var brain = new OthelloBrain(8, 8);
            var playerBlack = brain.GetBoard(0);
            var playerWhite = brain.GetBoard(1);
            var boardSize = new BoardSize();
            var startGame = new GameAction();
            return "";
        }
    }
}
