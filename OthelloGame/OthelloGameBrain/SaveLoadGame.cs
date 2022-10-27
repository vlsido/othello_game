using MenuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DAL.FileSystem;
using Domain;

namespace OthelloGameBrain
{
    public class SaveLoadGame
    {

        public string SaveGame(OthelloBrain brain, BoardSquareState[,] board, int axisX, int axisY, int blackScore, int whiteScore, string winner)
        {
            Console.WriteLine("Name saved game: ");
            var savedGameName = Console.ReadLine();

            // var gameConfJsonStr = JsonSerializer.Serialize(boardAConfig, jsonOptions);
            var gameBoardJsonStr = brain.GetBrainJson(brain, board);

            var saveMenu = new Menu("Where to save?", EMenuLevel.SecondOrMore);

            saveMenu.AddMenuItems(new List<MenuItem>()
            {
                //new("Database", SaveToDatabase),
                new("Locally", SaveLocally)
            });

            saveMenu.Run();

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

        public string LoadGame(OthelloBrain brain, BoardSquareState[,] board)
        {
            var repo = new GameRepositoryFileSystem();
            var games = repo.GetGames();


            var saveMenu = new Menu("Where to save?", EMenuLevel.SecondOrMore);

            foreach (var game in games)
            {
                saveMenu.AddMenuItems(new List<MenuItem>()
                {
                    //new("Database", SaveToDatabase),
                    new($"{game}", StartLoadedGame)
                });
                string StartLoadedGame()
                {
                    var gameAction = new GameAction();
                    var gameState = repo.GetGameState(game);
                    (brain, brain.GameBoard.Board) = brain.RestoreBrainFromJson(gameState.SerializedGameState, brain);
                    board = brain.GetBoard();
                    return gameAction.Start(brain, board, gameState.AxisX, gameState.AxisY, gameState.Winner,
                        gameState.WhiteScore, gameState.BlackScore);
                }
            }
            

            saveMenu.Run();

            

            return "";
        }

    }
}
