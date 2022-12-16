using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Othello_Web.Data;
using Othello_Web.Domain;
using OthelloGameBrain;
using static Humanizer.In;

namespace Othello_Web.Pages.OthelloGames
{
    public class PlayModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PlayModel(ApplicationDbContext context)
        {
            _context = context;
        }

       

        public OthelloBrain Brain { get; set; } = default!;
        public OthelloGameState? GameState { get; set; }
        public BoardSquareState[,] Board { get; set; } = default!;
        public List<List<BoardSquareState>> LinesOfSquares { get; set; } = default!;
        public ValidMoves ValidMoves { get; set; } = default!;
        public OthelloGame Game { get; set; } = default!;



        public string CurrentPlayer { get; set; } = "Black";

        public async Task<IActionResult> OnGet(int? id, int? x, int? y)
        {
            
            var game = _context.OthelloGames
                .Include(o => o.OthelloOption)
                .Include(gs => gs.OthelloGameStates)
                .FirstOrDefault(g => g.Id == id);

            if (game == null || game.OthelloOption == null || game.OthelloGameStates == null)
            {
                return NotFound();
            }

            var serialized = "";

            Brain = new OthelloBrain(game.OthelloOption.Width, game.OthelloOption.Height)
            {
                BoardSizeHorizontal = game.OthelloOption.Width,
                BoardSizeVertical = game.OthelloOption.Height
            };

            Board = Brain.GetBoard();
            Game = game;
            if (_context.OthelloGamesStates.FirstOrDefaultAsync(g => g.OthelloGameId == id!.Value) == null)
            {
                

                serialized = Brain.GetBrainJson(Brain, Board);
                _context.OthelloGamesStates.Add(new OthelloGameState()
                {
                    AxisX = 0,
                    AxisY = 0,
                    BlackScore = 2,
                    WhiteScore = 2,
                    OthelloGameId = id!.Value,
                    SerializedGameState = serialized,
                });
                await _context.SaveChangesAsync();
            }
            else
            {
                GameState = _context.OthelloGamesStates.FirstOrDefault(og => og.OthelloGameId == id!.Value);
                (Brain, Board) = Brain.RestoreBrainFromJson(GameState!.SerializedGameState, Brain);

            }

            LinesOfSquares = new List<List<BoardSquareState>>();

            ValidMoves = new ValidMoves();

            (Board, LinesOfSquares) = ValidMoves.CheckValidMoves(Brain, Board, LinesOfSquares);

            foreach (var firstDimension in LinesOfSquares)
            {
                foreach (var secondDimension in firstDimension)
                {
                    if (secondDimension.IsValid)
                    {
                        Board[secondDimension.X, secondDimension.Y].IsValid = true;
                    }
                }
            }

            if (x != null && y != null)
            {
                if (Board[x.Value, y.Value].IsValid)
                {
                    Board[x.Value, y.Value].IsValid = false;
                    Board[x.Value, y.Value].IsPlaced = true;
                    if (GameState != null && GameState.CurrentMoveByBlack)
                    {
                        Board[x.Value, y.Value].PlayerColor = "Black";
                        CurrentPlayer = "White";
                        GameState.CurrentMoveByBlack = false;
                    }
                    else
                    {
                        Board[x.Value, y.Value].PlayerColor = "White";
                        GameState!.CurrentMoveByBlack = true;
                        CurrentPlayer = "Black";
                    }
                }
            }

            var serializedStr = Brain.GetBrainJson(Brain, Board);

            GameState!.SerializedGameState = serializedStr;


            await using ApplicationDbContext ctx = new ApplicationDbContext(options);
            ctx.OthelloGamesStates.Update(GameState);
            await ctx.SaveChangesAsync();

            return Page();
        }
    }
}
