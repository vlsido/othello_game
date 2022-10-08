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
            var board = brain.GetBoard();
            var boardSize = new BoardSize();
            var settings = new Options();
            // if default settings
           settings.DefaultOptions(board);
            // TODO: if not default settings
            
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
                game.Start(brain, board, boardSize);
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
