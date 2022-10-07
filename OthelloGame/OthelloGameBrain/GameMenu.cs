using MenuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO: implement "start game" method
namespace OthelloGameBrain
{
    public class GameMenu
    {
        public void MainMenu()
        {
            var brain = new OthelloBrain(8, 8);
            var playerBlack = brain.GetBoard(0);
            var playerWhite = brain.GetBoard(1);
            var boardSize = new BoardSize();

            //Console.Clear();
            var mainMenu = new Menu("Othello Game", EMenuLevel.Root);
            mainMenu.AddMenuItems(new List<MenuItem>()
            {
                new MenuItem("New game", SubmenuNewGame),
                new MenuItem("Load game", SubmenuLoadGame),
            });

            mainMenu.Run();

            string SubmenuNewGame()
            {
                var menu = new Menu("New game", EMenuLevel.First);
                menu.AddMenuItems(new List<MenuItem>()
                {
                    new MenuItem("Start", StartGame),
                    new MenuItem("Options", Options),
                });
                var res = menu.Run();
                return res;
            }

            string SubmenuLoadGame()
            {
                var menu = new Menu("New game", EMenuLevel.First);
                menu.AddMenuItems(new List<MenuItem>()
                {
                    new MenuItem("Saved Games", SavedGames),
                    new MenuItem("Options", Options),
                });
                var res = menu.Run();
                return res;
            }

            string StartGame()
            {
                var game = new GameAction();
                game.Start(brain, playerBlack, playerWhite, boardSize);
                return "";
            }

            string SavedGames()
            {
                Console.WriteLine("Enjoy your game!");
                return "";
            }

            string Options()
            {
                Console.WriteLine("Enjoy your game!");
                return "";
            }
        }

        
    }
}
