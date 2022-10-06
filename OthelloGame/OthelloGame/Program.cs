using MenuSystem;

namespace OthelloGameConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
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
            Console.WriteLine("Enjoy your game!");
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