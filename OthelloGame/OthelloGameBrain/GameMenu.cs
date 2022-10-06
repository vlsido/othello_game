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
            Console.Clear();
            var mainMenu = new Menu("Othello Game", EMenuLevel.Root);
            mainMenu.AddMenuItems(new List<MenuItem>()
            {
                new MenuItem("New game", SubmenuNewGame),
                new MenuItem("Load game", SubmenuLoadGame),
            });

            mainMenu.Run();
        }

        public static string SubmenuNewGame()
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

        public static string SubmenuLoadGame()
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

        public static string StartGame()
        {

            return "";
        }

        public static string SavedGames()
        {
            Console.WriteLine("Enjoy your game!");
            return "";
        }

        public static string Options()
        {
            Console.WriteLine("Enjoy your game!");
            return "";
        }
    }
}
