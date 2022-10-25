using MenuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.FileSystem;


namespace OthelloGameBrain
{
    public class GameMenu
    {
        public void MainMenu()
        {
            //
            IGameOptionsRepository repo = new GameOptionsRepositoryFileSystem();
            var options = repo.GetGameOptionsList(); 
            foreach (var option in options)
            {
                Console.WriteLine(option);
            }

            Thread.Sleep(100000);
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
                int axisX = 0;
                int axisY = 0;
                int whiteScore = 0;
                int blackScore = 0;
                string winner = "null";
                var game = new GameAction();
                game.Start(brain, board, boardSize, axisX, axisY, winner, blackScore, whiteScore);
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
