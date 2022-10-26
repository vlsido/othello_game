using MenuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.FileSystem;
using Domain;


namespace OthelloGameBrain
{
    public class GameMenu
    {
        public string MainMenu()
        {

            IGameOptionsRepository repo = new GameOptionsRepositoryFileSystem();
            var othelloOptions = repo.GetGameOptions("Default options");
            var brain = new OthelloBrain(othelloOptions.Width, othelloOptions.Height);
            var board = brain.GetBoard();
            // if default settings
            brain.BoardSizeHorizontal = othelloOptions.Width;
            brain.BoardSizeVertical = othelloOptions.Height;
            brain.CurrentPlayer = othelloOptions.CurrentPlayer;
            //board = othelloOptions.SetBoardInitialPieces(board);
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
                return "";
            }

            string StartGame()
            {
                int axisX = 0;
                int axisY = 0;
                int whiteScore = 0;
                int blackScore = 0;
                string winner = "null";
                var game = new GameAction();
                game.Start(brain, board, axisX, axisY, winner, blackScore, whiteScore);
                return "";
            }

            string SavedGames()
            {
                Console.WriteLine("Enjoy your game!");
                return "";
            }

            string Options()
            {
                
                
                var optionsList = repo.GetGameOptionsList();
                

                var menu = new Menu("New game", EMenuLevel.First);
                foreach (var option in optionsList)
                {
                    menu.AddMenuItems(new List<MenuItem>()
                    {
                        new MenuItem($"{option}", LoadOption),
                    });
                    string LoadOption()
                    {
                        othelloOptions = repo.GetGameOptions($"{option}");
                        brain = new OthelloBrain(othelloOptions.Width, othelloOptions.Height);
                        board = brain.GetBoard();
                        brain.CurrentPlayer = othelloOptions.CurrentPlayer;
                        brain.BoardSizeHorizontal = othelloOptions.Width;
                        brain.BoardSizeVertical = othelloOptions.Height;
                        
                        return StartGame();
                    }
                }
                
                var res = menu.Run();
                return res;

                
            }

            return "";
        }

    }
}
