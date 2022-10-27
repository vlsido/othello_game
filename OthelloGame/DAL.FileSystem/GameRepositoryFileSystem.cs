using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FileSystem
{
    public class GameRepositoryFileSystem
    {
        private const string FileExtension = "json";
        private readonly string _savedGamesDirectory = @"D:\othellogame\OthelloGame\DAL.FileSystem" + System.IO.Path.DirectorySeparatorChar + "OthelloSavedGames";
        
        public string Name { get; } = "FileSystem";

        public List<string> GetGames()
        {
            CheckOrCreateDirectory();

            var res = new List<string>();

            foreach (var fileName in Directory.GetFileSystemEntries(_savedGamesDirectory, "*." + FileExtension))
            {
                res.Add(System.IO.Path.GetFileNameWithoutExtension(fileName));
            }

            return res;
        }

        public void SaveGame(string id, OthelloGameState gameState)
        {
            CheckOrCreateDirectory();

            var fileContent = System.Text.Json.JsonSerializer.Serialize(gameState);
            System.IO.File.WriteAllText(GetFileName(id), fileContent);
            Console.WriteLine($"Saved to: {_savedGamesDirectory}");
        }

        public void DeleteGame(string id)
        {
            System.IO.File.Delete(GetFileName(id));
        }

        public OthelloGameState GetGameState(string id)
        {
            var fileContent = System.IO.File.ReadAllText(GetFileName(id));
            var savedGame = System.Text.Json.JsonSerializer.Deserialize<OthelloGameState>(fileContent);
            if (savedGame == null)
            {
                throw new NullReferenceException($"Could not deserialize: {fileContent}");
            }

            return savedGame;
        }

        private string GetFileName(string id)
        {
            return _savedGamesDirectory +
                   System.IO.Path.DirectorySeparatorChar +
                   id + "." + FileExtension;
        }

        private void CheckOrCreateDirectory()
        {
            if (!System.IO.Directory.Exists(_savedGamesDirectory))
            {
                System.IO.Directory.CreateDirectory(_savedGamesDirectory);
            }
        }
    }

}
