using MenuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DAL.Db;
using DAL.FileSystem;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace OthelloGameBrain
{
    public class SaveLoadGame
    {

        public string SaveGame(OthelloBrain brain, BoardSquareState[,] board, int axisX, int axisY, int blackScore, int whiteScore, string winner,
            AppDbContext othelloDb)
        {
            Console.WriteLine("Name saved game: ");
            var savedGameName = Console.ReadLine();

            // var gameConfJsonStr = JsonSerializer.Serialize(boardAConfig, jsonOptions);
            var gameBoardJsonStr = brain.GetBrainJson();

            var saveMenu = new Menu("Where to save?", EMenuLevel.SecondOrMore);

            saveMenu.AddMenuItems(new List<MenuItem>()
            {
                new("Database", SaveToDatabase),
                new("Locally", SaveLocally),
            });

            saveMenu.Run();


            string SaveToDatabase()
            {
                var gameState = new OthelloGameState()
                {
                    AxisX = axisX,
                    AxisY = axisY,
                    BlackScore = blackScore,
                    WhiteScore = whiteScore,
                    Winner = winner,
                    OthelloGameId = brain.GameId,
                    SerializedGameState = gameBoardJsonStr
                };


                othelloDb.OthelloGamesStates.Update(gameState);

                othelloDb.SaveChanges();

                foreach (var othelloGameState in othelloDb.OthelloGamesStates)
                {
                    Console.WriteLine(othelloGameState);
                }

                return "";
            }

            string SaveLocally()
            {
                var othelloGameState = new OthelloGameState();
                var repo = new GameRepositoryFileSystem();

                othelloGameState.CreatedAt = DateTime.Now;
                othelloGameState.SerializedGameState = gameBoardJsonStr;
                othelloGameState.AxisX = axisX;
                othelloGameState.AxisY = axisY;
                othelloGameState.BlackScore = blackScore;
                othelloGameState.WhiteScore = whiteScore;
                othelloGameState.Winner = winner;
                if (savedGameName != null) repo.SaveGame(savedGameName, othelloGameState);


                return "";
            }

            return "";
        }

        public string LoadGame(OthelloBrain brain, BoardSquareState[,] board, AppDbContext othelloDb)
        {
            var repo = new GameRepositoryFileSystem();
            var games = repo.GetGames();


            var loadMenu = new Menu("Where to load from?", EMenuLevel.SecondOrMore);

            loadMenu.AddMenuItems(new List<MenuItem>()
                {
                    //new("Database", SaveToDatabase),
                    new("Local Storage", LocalStorage),
                    new("Database", Database)
                });

            loadMenu.Run();

            string LocalStorage()
            {
                var localStorage = new Menu("Choose from Local Storage", EMenuLevel.SecondOrMore);
                foreach (var game in games)
                {
                    localStorage.AddMenuItems(new List<MenuItem>()
                {
                    //new("Database", SaveToDatabase),
                    new($"{game}", StartLoadedGame)
                });
                    string StartLoadedGame()
                    {
                        var gameAction = new GameAction();
                        var gameState = repo.GetGameState(game);
                        (brain, board) = brain.RestoreBrainFromJson(gameState.SerializedGameState, brain);

                        return gameAction.Start(brain, board, gameState.AxisX, gameState.AxisY, gameState.Winner!,
                            gameState.WhiteScore, gameState.BlackScore, othelloDb);
                    }
                }


                localStorage.Run();



                return "";
            }

            string Database()
            {
                var database = new Menu("Choose from Database", EMenuLevel.SecondOrMore);
                foreach (var game in othelloDb.OthelloGames)
                {
                    database.AddMenuItems(new List<MenuItem>()
                {
                    new($"{game.Id}.{game.Player1Name} vs {game.Player2Name}", StartLoadedGame)
                });
                    string StartLoadedGame()
                    {
                        var gameAction = new GameAction();
                        var gameState = othelloDb.OthelloGamesStates.FirstOrDefault(g => g.OthelloGameId == game.Id);
                        (brain, board) = brain.RestoreBrainFromJson(gameState!.SerializedGameState, brain);
                       
                        return gameAction.Start(brain, board, gameState.AxisX, gameState.AxisY, gameState.Winner!,
                            gameState.WhiteScore, gameState.BlackScore, othelloDb);
                    }
                }
                database.Run();
                return "";
            }
            return "";
        }

    }
}
