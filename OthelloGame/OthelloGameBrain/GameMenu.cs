using MenuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Db;
using DAL.FileSystem;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace OthelloGameBrain
{
    public class GameMenu
    {
        public string MainMenu()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=D:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;

            using var othelloDb = new AppDbContext(options);

            var saveLoad = new SaveLoadGame();
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
                return saveLoad.LoadGame(brain, board, othelloDb);
            }

            string StartGame()
            {
                int axisX = 0;
                int axisY = 0;
                int whiteScore = 0;
                int blackScore = 0;
                string winner = "null";
                var game = new GameAction();
#pragma warning disable CS0219
                EPlayerType opponentType;
#pragma warning restore CS0219

                var playAgainst = new Menu("Play against:", EMenuLevel.First);
                playAgainst.AddMenuItems(new List<MenuItem>()
                {
                    new MenuItem("Human", PlayAgainstHuman),
                    new MenuItem("Ai", PlayAgainstAi),
                });
                var res = playAgainst.Run();

                string PlayAgainstHuman()
                {
                    Console.Write("Enter Player1 name: ");
                    var player1Name = Console.ReadLine();
                    Console.Write("Enter Player2 name: ");
                    var player2Name = Console.ReadLine();
                    opponentType = EPlayerType.Human;

                    if (player1Name != null && player2Name != null)
                    {
                        var othelloGame = new OthelloGame()
                        {
                            Player1Name = player1Name,
                            Player1Type = EPlayerType.Human,
                            Player2Name = player2Name,
                            Player2Type = EPlayerType.Human,
                            OthelloOption = othelloOptions,
                            OthelloGameStates = new List<OthelloGameState>()
                            {
                                new OthelloGameState()
                                {
                                    AxisX = axisX,
                                    AxisY = axisY,
                                    BlackScore = blackScore,
                                    WhiteScore = whiteScore,
                                    SerializedGameState = brain.GetBrainJson(brain, board),
                                    Winner = winner
                                }
                            }
                        };

                        othelloDb.OthelloGames.Add(othelloGame);
                    }

                    othelloDb.SaveChanges();

                    brain.OpponentType = EPlayerType.Human;

                    return game.Start(brain, board, axisX, axisY, winner, blackScore, whiteScore, othelloDb);
                }

                string PlayAgainstAi()
                {
                    Console.Write("Enter your name: ");
                    var player1Name = Console.ReadLine();
                    var player2Name = "Bot";
                    opponentType = EPlayerType.Ai;

                    if (player1Name != null)
                    {
                        var othelloGame = new OthelloGame()
                        {
                            Player1Name = player1Name,
                            Player1Type = EPlayerType.Human,
                            Player2Name = player2Name,
                            Player2Type = EPlayerType.Ai,
                            OthelloOption = othelloOptions,
                            OthelloGameStates = new List<OthelloGameState>()
                            {
                                new OthelloGameState()
                                {
                                    AxisX = axisX,
                                    AxisY = axisY,
                                    BlackScore = blackScore,
                                    WhiteScore = whiteScore,
                                    SerializedGameState = brain.GetBrainJson(brain, board),
                                    Winner = winner
                                }
                            }
                        };

                        othelloDb.OthelloGames.Add(othelloGame);
                    }

                    othelloDb.SaveChanges();
                    
                    brain.OpponentType = EPlayerType.Ai;

                    return game.Start(brain, board, axisX, axisY, winner, blackScore, whiteScore, othelloDb);
                }

                return "";
            }

            string SaveGame()
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

                return menu.Run();
            }

            return "";
        }

    }
}
