using MenuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OthelloGameBrain
{
    public class SaveLoadGame
    {
        public static string BasePath = default!;

        //public string SaveGame(string[] args, OthelloBrain brain, BoardSquareState[,] board)
        //{
        //    BasePath = args.Length == 1 ? args[0] : Directory.GetCurrentDirectory();

        //    var jsonOptions = new JsonSerializerOptions()
        //    {
        //        WriteIndented = true
        //    };
        //    Console.WriteLine("Name saved game: ");
        //    var savedGameName = Console.ReadLine();
        //    var dir = BasePath + Path.DirectorySeparatorChar + "SavedGames" +
        //              Path.DirectorySeparatorChar + savedGameName;
        //    if (!Directory.Exists(dir))
        //    {
        //        Directory.CreateDirectory(dir);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Game with this name already exists!");
        //        return "";
        //    }

        //    var fileNameCurrentBoardState = BasePath + Path.DirectorySeparatorChar + "SavedGames" +
        //                                    Path.DirectorySeparatorChar + savedGameName + Path.DirectorySeparatorChar +
        //                                    "boardState.json";

        //    // var gameConfJsonStr = JsonSerializer.Serialize(boardAConfig, jsonOptions);
        //    var gameBoardJsonStr = brain.GetBrainJson(brain, board);

        //    var saveMenu = new Menu("Where to save?", EMenuLevel.SecondOrMore);

        //    saveMenu.AddMenuItems(new List<MenuItem>()
        //    {
        //        new("Database", SaveToDatabase),
        //        new("Locally", SaveLocally)
        //    });


        //}

    }
}
