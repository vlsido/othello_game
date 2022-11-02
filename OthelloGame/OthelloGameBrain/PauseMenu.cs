using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Db;
using MenuSystem;

namespace OthelloGameBrain
{
    public class PauseMenu
    {
        public void PauseMenuFunction( OthelloBrain brain, BoardSquareState[,] board,
            List<List<BoardSquareState>> linesOfSquares, int axisX, int axisY, string winner, int blackScore, int whiteScore,
            AppDbContext othelloDb)
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
                return saveLoadGame.SaveGame(brain, board, axisX, axisY, blackScore, whiteScore, winner, othelloDb);
            }

            string LoadGameFunction()
            {
                return saveLoadGame.LoadGame(brain, board, othelloDb);
            }

            string MainMenuFunction()
            {
                return gameMenu.MainMenu();
            }


        }
    }
}
