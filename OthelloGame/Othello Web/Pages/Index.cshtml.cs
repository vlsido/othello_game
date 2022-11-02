using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Othello_Web.Data;
using Othello_Web.Domain;

namespace Othello_Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _othelloDb;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext othelloDb)
        {
            _logger = logger;
            _othelloDb = othelloDb;
        }

        public OthelloGame Game { get; set; } = default!;

        public void OnGet()
        {
            //_othelloDb.OthelloGames.Add(new OthelloGame()
            //{
            //    Player1Name = "Vlad",
            //    Player1Type = EPlayerType.Human,
            //    Player2Name = "Anastasia",
            //    Player2Type = EPlayerType.Human,
            //    OthelloOption = new OthelloOption()
            //    {
            //        CurrentPlayer = "Black",
            //        Height = 8,
            //        Width = 8,
            //        Name = "Default"
            //    },
            //    OthelloGameStates = new List<OthelloGameState>()
            //    {
            //        new OthelloGameState()
            //        {
            //            AxisX = 0,
            //            AxisY = 0,
            //            BlackScore = 2,
            //            WhiteScore = 2,
            //            SerializedGameState = "none"
            //        }
            //    }
            //});
            //_othelloDb.SaveChanges();

            Game = _othelloDb.OthelloGames.First();
        }
    }
}