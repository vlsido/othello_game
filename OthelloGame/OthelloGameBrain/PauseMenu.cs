using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuSystem;

namespace OthelloGameBrain
{
    public class PauseMenu
    {
        public void PauseMenuFunction(string[] args, OthelloBrain brain, BoardSquareState[,] board,
            List<List<BoardSquareState>> linesOfSquares, int axisX, int axisY, string winner, int blackScore, int whiteScore)
        {
            var gameMenu = new GameMenu();
            var saveLoadGame = new SaveLoadGame();
            PauseMenu();

            string PauseMenu()
            {
                var menu = new Menu("Pause", EMenuLevel.Root);
                menu.AddMenuItems(new List<MenuItem>()
                {
                    new("Save Game", SaveGameFunction),
                    new("Load Game", LoadGameFunction),
                    new("Main menu", MainMenuFunction)
                });

                return menu.Run();
            }

            string SaveGameFunction()
            {
                saveLoadGame.SaveGame(args, boardA!, boardAHidden!,
                    boardB!, boardBHidden!, brain!, aDict, aCoords, bDict, bCoords, db);
                var res = "";
                return res;
            }

            string LoadGameFunction()
            {
                saveLoadGame.LoadGame(args, brain, boardA!, boardAHidden!, boardB!, boardBHidden!, aDict, aCoords,
                    bDict, bCoords, db);
                var res = "";
                return res;
            }

            string MainMenuFunction()
            {
                return gameMenu.MainMenu();
            }


        }
    }
}
