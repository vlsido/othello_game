

namespace DAL;

public interface IGameOptionsRepository
{

    string Name { get; }

    // crud methods

    // read
    List<string> GetGameOptionsList();
    //OthelloOption GetGameOptions(string id);

    //// create and update
    //void SaveGameOptions(string id, OthelloOption option);

    //// delete
    //void DeleteGameOptions(string id);
}