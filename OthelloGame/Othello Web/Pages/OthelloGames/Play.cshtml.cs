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
        public OthelloGameState? BlackGameState { get; set; }
        public OthelloGameState? WhiteGameState { get; set; }
        public OthelloOption OthelloOption { get; set; } = default!;
        public BoardSquareState[,] Board { get; set; } = default!;
        public List<List<BoardSquareState>> LinesOfSquares { get; set; } = default!;
        public ValidMoves ValidMoves { get; set; } = default!;
        public CountScore CountScore { get; set; } = new CountScore();
        public OthelloGame Game { get; set; } = default!;
        public string Player { get; set; } = default!;

        public async Task<IActionResult> OnGet(int? id, int? x, int? y, string? player)
        {
            var game = _context.OthelloGames
                .Include(o => o.OthelloOption)
                .Include(gs => gs.OthelloGameStates)
                .FirstOrDefault(g => g.Id == id);

            BoardSquareState chosenByAi = new BoardSquareState();
            List<BoardSquareState> validForAi = new List<BoardSquareState>();
            var _rnd = new Random();

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



            if (_context.OthelloGamesStates.FirstOrDefault(g =>
                    g.OthelloGameId == id!.Value && g.Perspective == "player1") == null)
            {
                var serialized = Brain.GetBrainJson(Brain, Board);

                GameState = new OthelloGameState()
                {
                    BlackScore = 2,
                    WhiteScore = 2,
                    Perspective = "player1",
                    OthelloGameId = id!.Value,
                    Winner = "null",
                    SerializedGameState = serialized,
                };
                if (game.OthelloOption.CurrentPlayer == "Black")
                {
                    GameState.CurrentMoveByBlack = true;
                }
                else
                {
                    GameState.CurrentMoveByBlack = false;
                }

                _context.OthelloGamesStates.Add(GameState);
                await _context.SaveChangesAsync();
            }

            if (_context.OthelloGamesStates.FirstOrDefault(g =>
                    g.OthelloGameId == id!.Value && g.Perspective == "player2") == null)
            {
                var serialized = Brain.GetBrainJson(Brain, Board);


                GameState = new OthelloGameState()
                {
                    BlackScore = 2,
                    WhiteScore = 2,
                    Perspective = "player2",
                    OthelloGameId = id!.Value,
                    Winner = "null",
                    SerializedGameState = serialized,
                };
                if (game.OthelloOption.CurrentPlayer == "Black")
                {
                    GameState.CurrentMoveByBlack = true;
                }
                else
                {
                    GameState.CurrentMoveByBlack = false;
                }

                _context.OthelloGamesStates.Add(GameState);
                await _context.SaveChangesAsync();
            }

            GameState = _context.OthelloGamesStates.FirstOrDefault(gs =>
                gs.OthelloGameId == id!.Value && gs.Perspective == player);
            (Brain, Board) = Brain.RestoreBrainFromJson(GameState!.SerializedGameState, Brain);
            LinesOfSquares = new List<List<BoardSquareState>>();
            ValidMoves = new ValidMoves();
            (Board, LinesOfSquares) = ValidMoves.CheckValidMoves(Brain, Board, LinesOfSquares);

            ShowValidMoves();

            const int leftSquares = 0;

            

            if (x != null && y != null)
            {
                if (GameState != null)
                    if (GameState.Perspective == "player1" && GameState.CurrentMoveByBlack &&
                        game.Player1Type != EPlayerType.Ai)
                    {
                        if (Board[x.Value, y.Value].IsValid)
                        {
                            Board[x.Value, y.Value].IsPlaced = true;
                            Board[x.Value, y.Value].IsValid = false; // check if needed
                            Board[x.Value, y.Value].PlayerColor = Brain.CurrentPlayer;
                            GameState.CurrentMoveByBlack = false;
                            var opponentGameState = _context.OthelloGamesStates.FirstOrDefault(gs =>
                                gs.OthelloGameId == id!.Value && gs.Perspective == "player2");
                            if (opponentGameState != null)
                            {
                                opponentGameState.CurrentMoveByBlack = false;
                                _context.OthelloGamesStates.Update(opponentGameState);
                            }

                            TurnPieces();
                            (Board, LinesOfSquares) = ValidMoves.CheckValidMoves(Brain, Board, LinesOfSquares);
                            ShowValidMoves();
                            Brain.CurrentPlayer = GameState.CurrentMoveByBlack ? "Black" : "White";
                            var serializedStr = Brain.GetBrainJson(Brain, Board);

                            var gameStates = _context.OthelloGamesStates.Where(og => og.OthelloGameId == id!.Value);

                            foreach (var gameState in gameStates)
                            {
                                if (gameState.Perspective == player)
                                {
                                    GameState = gameState;
                                }

                                gameState.SerializedGameState = serializedStr;
                                _context.OthelloGamesStates.Update(gameState);
                            }
                        }

                    }
                    else if (GameState.Perspective == "player2" && !GameState.CurrentMoveByBlack &&
                             game.Player2Type != EPlayerType.Ai)
                    {
                        if (Board[x.Value, y.Value].IsValid)
                        {
                            Board[x.Value, y.Value].IsPlaced = true;
                            Board[x.Value, y.Value].IsValid = false; // check if needed
                            Board[x.Value, y.Value].PlayerColor = Brain.CurrentPlayer;
                            GameState.CurrentMoveByBlack = true;
                            _context.OthelloGamesStates.Update(GameState);
                            var opponentGameState = _context.OthelloGamesStates.FirstOrDefault(gs =>
                                gs.OthelloGameId == id!.Value && gs.Perspective == "player1");
                            if (opponentGameState != null)
                            {
                                opponentGameState.CurrentMoveByBlack = true;
                                _context.OthelloGamesStates.Update(opponentGameState);
                            }

                            (Board, LinesOfSquares) = ValidMoves.CheckValidMoves(Brain, Board, LinesOfSquares);
                            ShowValidMoves();
                            TurnPieces();
                            Brain.CurrentPlayer = GameState.CurrentMoveByBlack ? "Black" : "White";

                            var serializedStr = Brain.GetBrainJson(Brain, Board);

                            var gameStates = _context.OthelloGamesStates.Where(og => og.OthelloGameId == id!.Value);

                            foreach (var gameState in gameStates)
                            {
                                if (gameState.Perspective == player)
                                {
                                    GameState = gameState;
                                }

                                gameState.SerializedGameState = serializedStr;
                                _context.OthelloGamesStates.Update(gameState);
                            }
                        }
                    }
            }

            (GameState!.WhiteScore, GameState.BlackScore, GameState.Winner) =
                CountScore.Score(Board, GameState.BlackScore, GameState.WhiteScore, GameState.Winner, leftSquares);

            (Board, LinesOfSquares) = ValidMoves.CheckValidMoves(Brain, Board, LinesOfSquares);
            ShowValidMoves();

            if (game.Player1Type == EPlayerType.Ai || game.Player2Type == EPlayerType.Ai)
            {
                if (GameState != null && GameState.CurrentMoveByBlack && game.Player1Type == EPlayerType.Ai)
                {
                    MakeAiMove("Black");
                    return Page();
                }

                if (GameState != null && !GameState.CurrentMoveByBlack && game.Player2Type == EPlayerType.Ai)
                {
                    MakeAiMove("White");
                    
                    return Page();
                }
            }

            if (GameState!.Winner != "null")
            {
                Game.GameOverAt = DateTime.Now;
                _context.OthelloGames.Update(Game);
            }

            await _context.SaveChangesAsync();


            return Page();

            void TurnPieces()
            {
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
            }

            void ShowValidMoves()
            {
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

            void MakeAiMove(string aiColor)
            {
                Thread.Sleep(750);
                ShowValidMoves();
                for (var j = 0; j < LinesOfSquares.Count; j++)
                {
                    for (var n = 0; n < LinesOfSquares[j].Count; n++)
                    {
                        if (LinesOfSquares[j][n].IsValid == true)
                        {
                            validForAi.Add(LinesOfSquares[j][n]);
                        }

                    }
                }

                var chosenFromList = _rnd!.Next(0, validForAi.Count);
                chosenByAi = validForAi[chosenFromList];
                chosenByAi.PlayerColor = aiColor;
                Board[chosenByAi.X, chosenByAi.Y]
                    .IsPlaced = true;
                Board[chosenByAi.X, chosenByAi.Y]
                    .PlayerColor = aiColor;


                for (var j = 0; j < LinesOfSquares.Count; j++)
                {
                    for (var n = 0; n < LinesOfSquares[j].Count; n++)
                    {
                        if (LinesOfSquares[j][n].X == chosenByAi.X && LinesOfSquares[j][n].Y == chosenByAi.Y)
                        {
                            foreach (var item in LinesOfSquares[j])
                            {
                                if (item.IsValid)
                                {
                                    Board[item.X, item.Y].IsPlaced = true;
                                    Board[item.X, item.Y].PlayerColor = aiColor;
                                }

                                if (item.PlayerColor != aiColor)
                                {
                                    Board[item.X, item.Y].PlayerColor = aiColor;
                                }
                            }

                        }
                    }
                }

                Brain.CurrentPlayer = aiColor == "Black" ? "White" : "Black";
                if (GameState.CurrentMoveByBlack)
                {
                    GameState.CurrentMoveByBlack = false;
                }
                else if (!GameState.CurrentMoveByBlack)
                {
                    GameState.CurrentMoveByBlack = true;
                }

                (GameState!.WhiteScore, GameState.BlackScore, GameState.Winner) =
                    CountScore.Score(Board, GameState.BlackScore, GameState.WhiteScore, GameState.Winner, leftSquares);
                (Board, LinesOfSquares) = ValidMoves.CheckValidMoves(Brain, Board, LinesOfSquares);
                ShowValidMoves();

                var serializedString = Brain.GetBrainJson(Brain, Board);

                var gameStates = _context.OthelloGamesStates.Where(og => og.OthelloGameId == id!.Value);

                foreach (var gameState in gameStates)
                {
                    if (gameState.Perspective == player)
                    {
                        GameState = gameState;
                    }

                    gameState.SerializedGameState = serializedString;
                    _context.OthelloGamesStates.Update(gameState);

                }
                _context.SaveChanges();
            }
        }
    }
}
