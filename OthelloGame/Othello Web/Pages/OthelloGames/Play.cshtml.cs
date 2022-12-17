using System.Runtime.InteropServices;
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
        public CountScore CountScore { get; set; } = new CountScore();
        public OthelloGame Game { get; set; } = default!;

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

            Brain = new OthelloBrain(game.OthelloOption.Width, game.OthelloOption.Height)
            {
                BoardSizeHorizontal = game.OthelloOption.Width,
                BoardSizeVertical = game.OthelloOption.Height
            };

            Board = Brain.GetBoard();
            Game = game;
            if (_context.OthelloGamesStates.FirstOrDefault(g => g.OthelloGameId == id!.Value) == null)
            {
                var serialized = Brain.GetBrainJson(Brain, Board);
                GameState = new OthelloGameState()
                {
                    AxisX = 0,
                    AxisY = 0,
                    BlackScore = 2,
                    WhiteScore = 2,
                    OthelloGameId = id!.Value,
                    Winner = "null",
                    SerializedGameState = serialized,
                };
                _context.OthelloGamesStates.Add(GameState);
                await _context.SaveChangesAsync();
            }
            else
            {
                GameState = _context.OthelloGamesStates.FirstOrDefault(og => og.OthelloGameId == id!.Value);
                (Brain, Board) = Brain.RestoreBrainFromJson(GameState!.SerializedGameState, Brain);
            }

            GameState.Winner = "null";

                LinesOfSquares = new List<List<BoardSquareState>>();

            //
            var checkValidAgain = false;
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
                    Board[x.Value, y.Value].IsPlaced = true;
                    if (GameState.CurrentMoveByBlack)
                    {
                        Board[x.Value, y.Value].IsValid = false;
                        Board[x.Value, y.Value].PlayerColor = Brain.CurrentPlayer;
                        GameState.CurrentMoveByBlack = false;
                    } else if (!GameState.CurrentMoveByBlack)
                    {
                        Board[x.Value, y.Value].IsValid = false;
                        Board[x.Value, y.Value].PlayerColor = Brain.CurrentPlayer;
                        GameState.CurrentMoveByBlack = true;
                    }
                    for (var i = 0; i < LinesOfSquares.Count; i++)
                    {
                        for (var j = 0; j < LinesOfSquares[i].Count; j++)
                        {
                            if (LinesOfSquares[i][j].X == x.Value && LinesOfSquares[i][j].Y == y.Value)
                            {
                                foreach (var item in LinesOfSquares[i])
                                {
                                    if (item.IsValid)
                                    {
                                        Board[item.X, item.Y].IsPlaced = true;
                                        Board[item.X, item.Y].PlayerColor = Brain.CurrentPlayer;
                                    }

                                    if (item.PlayerColor != Brain.CurrentPlayer)
                                    {
                                        Board[item.X, item.Y].PlayerColor = Brain.CurrentPlayer;
                                    }
                                }
                            }
                        }
                    }
                    Brain.CurrentPlayer = GameState.CurrentMoveByBlack ? "Black" : "White";
                }
                checkValidAgain = true;
            }

            if (checkValidAgain)
            {
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
            }

            // COUNT CURRENT SCORE
            int leftSquares = 0;
            (GameState.BlackScore, GameState.WhiteScore, GameState.Winner) =
                CountScore.Score(Board, GameState.BlackScore, GameState.WhiteScore, GameState.Winner, leftSquares);

            var serializedStr = Brain.GetBrainJson(Brain, Board);

            GameState.SerializedGameState = serializedStr;

            _context.OthelloGamesStates.Update(GameState);
            await _context.SaveChangesAsync();
            //await _context.DisposeAsync();

            return Page();
        }
    }
}
