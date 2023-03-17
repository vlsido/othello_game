using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using DAL.Db;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace OthelloGameBrain
{
    public class OthelloBrain
    {
        public static string BasePath = "E:\\othellogame\\OthelloGame\\SavedGames";
        public string CurrentPlayer = "Black";
        public EPlayerType OpponentType;

        public GameBoard GameBoard;

        private readonly Random _rnd = new();

        public int GameId { get; set; }
        public int BoardSizeHorizontal { get; set; }
        public int BoardSizeVertical { get; set; } 

        public OthelloBrain(int boardSizeHorizontal, int boardSizeVertical)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(@"Data Source=E:\othellogame\OthelloGame\OthelloGame\othello.db")
                .Options;

            using var othelloDb = new AppDbContext(options);
            GameBoard = new GameBoard
            {
                Board = new BoardSquareState[boardSizeHorizontal, boardSizeVertical]
            };
            GameId = othelloDb.OthelloGames.Where(games => games.Id != 0).Count() + 1;
        }

        public BoardSquareState[,] GetBoard()
        {
            return CreateBoard(GameBoard.Board);
        }

        private BoardSquareState[,] CreateBoard(BoardSquareState[,] board)
        {
            var oneTimeFunc = true;
            var res = new BoardSquareState[board.GetLength(0), board.GetLength(1)];
            for (var x = 0; x < board.GetLength(0); x++)
            {
                for (var y = 0; y < board.GetLength(1); y++)
                {
                    if (oneTimeFunc)
                    {
                        board[(board.GetLength(0) / 2), (board.GetLength(1) / 2)].IsPlaced = true;
                        board[(board.GetLength(0) / 2), (board.GetLength(1) / 2)].PlayerColor = "White";
                        board[(board.GetLength(0) / 2) - 1, (board.GetLength(1) / 2) - 1].IsPlaced = true;
                        board[(board.GetLength(0) / 2) - 1, (board.GetLength(1) / 2) - 1].PlayerColor = "White";
                        board[(board.GetLength(0) / 2) - 1, (board.GetLength(1) / 2)].IsPlaced = true;
                        board[(board.GetLength(0) / 2) - 1, (board.GetLength(1) / 2)].PlayerColor = "Black";
                        board[(board.GetLength(0) / 2), (board.GetLength(1) / 2) - 1].IsPlaced = true;
                        board[(board.GetLength(0) / 2), (board.GetLength(1) / 2) - 1].PlayerColor = "Black";
                        oneTimeFunc = false;
                    }
                    board[x, 0].IsFileNotation = true;
                    res[x, y] = board[x, y];
                }
            }

            


            return res;
        }

        //public string GetBrainJson(OthelloBrain brain, BoardSquareState[,] board, ApplicationDbContext db)
        public string GetBrainJson()
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                AllowTrailingCommas = true
            };

            var dto = new GameDTO();

            for (var x = 0; x <= GameBoard.Board.GetLength(0); x++)
            {
                dto.GameBoard.Board.Add(new List<BoardSquareState>());
                for (var y = 0; y < GameBoard.Board.GetLength(1); y++)
                {
                    dto.GameBoard.Board[x].Add(new BoardSquareState());
                }
                

            }
            dto.CurrentPlayer = CurrentPlayer;

            for (var x = 0; x < GameBoard.Board.GetLength(0); x++)
            {
                for (var y = 0; y < GameBoard.Board.GetLength(1); y++)
                {
                    dto.GameBoard.Board[x][y] = GameBoard.Board[x, y];
                }
            }

            dto.BoardSizeHorizontal = BoardSizeHorizontal;
            dto.BoardSizeVertical = BoardSizeVertical;

            var jsonStr = JsonSerializer.Serialize(dto, jsonOptions);

            //var dbBrain = new Domain.BsBrain();

            //dbBrain.GameString = jsonStr;
            //db.BsBrains.Add(dbBrain);
            //db.SaveChanges();

            return jsonStr;

        }

        public GameDTO BrainToBrainDTO (OthelloBrain brain)
        {
            var dto = new GameDTO();

            dto.GameBoard = new GameDTO.GameBoardDTO(new List<List<BoardSquareState>>());

            if (brain != null)
            {
                dto.CurrentPlayer = brain.CurrentPlayer;
                for (var x = 0; x < brain.GameBoard.Board.GetLength(0); x++)
                {
                    for (var y = 0; y < brain.GameBoard.Board.GetLength(1); y++)
                    {
                        dto.GameBoard.Board[x][y] = brain.GameBoard.Board[x, y];
                    }
                }
            }

            return dto;
        }

        public (OthelloBrain, BoardSquareState[,]) RestoreBrainFromJson(string jsonStr, OthelloBrain brain)
        {
            var board = brain.GetBoard();
            var boardSizeHorizontal = 8;
            var boardSizeVertical = 8;

            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                AllowTrailingCommas = true
            };


            var dto = JsonSerializer.Deserialize<GameDTO>(jsonStr, jsonOptions);

            if (dto != null)
            {
                boardSizeHorizontal = dto.BoardSizeHorizontal;
                boardSizeVertical = dto.BoardSizeVertical;
            }
            else
            {
                Console.WriteLine("Whoops, something went wrong...");
                return (brain, board)!;
            }

            brain.GameBoard.Board = new BoardSquareState[boardSizeHorizontal, boardSizeVertical];
            brain.BoardSizeHorizontal = boardSizeHorizontal;
            brain.BoardSizeVertical = boardSizeVertical;

            board = brain.GetBoard();

            brain.CurrentPlayer = dto.CurrentPlayer;

            for (var x = 0; x < board.GetLength(0); x++)
            {
                for (var y = 0; y < board.GetLength(1); y++)
                {
                    board[x, y] = dto.GameBoard.Board[x][y];
                }
            }
            
            return (brain, board)!;
        }

        public bool MakeAMove(bool web, int id, OthelloGame game, OthelloBrain brain, OthelloGameState gameState, BoardSquareState[,] board, int axisX, int axisY, 
            string playerColor, List<List<BoardSquareState>> linesOfSquares, bool movedone,
            AppDbContext othelloDb, string player)
        {
            ValidMoves valid = new ValidMoves();
            if (!web)
            {
                if (board[axisX, axisY].IsValid)
                {
                    board[axisX, axisY].IsPlaced = true;
                    board[axisX, axisY].PlayerColor = playerColor;

                   
                    TurnPieces();

                    return movedone = true; 
                  
                }
            } else if (web)
            {
                if (gameState.Perspective == "player1" && gameState.CurrentMoveByBlack &&
                        game.Player1Type != EPlayerType.Ai)
                {
                    if (board[axisX, axisY].IsValid)
                    {
                        board[axisX, axisY].IsPlaced = true;
                        board[axisX, axisY].IsValid = false; // check if needed
                        board[axisX, axisY].PlayerColor = brain.CurrentPlayer;
                        gameState.CurrentMoveByBlack = false;
                        var opponentGameState = othelloDb.OthelloGamesStates.FirstOrDefault(gs =>
                            gs.OthelloGameId == id! && gs.Perspective == "player2");
                        if (opponentGameState != null)
                        {
                            opponentGameState.CurrentMoveByBlack = false;
                            othelloDb.OthelloGamesStates.Update(opponentGameState);
                        }
                        

                        (board, linesOfSquares) = valid.CheckValidMoves(brain, board, linesOfSquares);
                        ShowValidMoves();
                        TurnPieces();
                      
                        SaveGameStates();
                        othelloDb.SaveChanges();
                        return movedone = true;
                    }
                   
                }
                else if (gameState.Perspective == "player2" && !gameState.CurrentMoveByBlack &&
                        game.Player2Type != EPlayerType.Ai)
                {
                    if (board[axisX, axisY].IsValid)
                    {
                        board[axisX, axisY].IsPlaced = true;
                        board[axisX, axisY].IsValid = false; // check if needed
                        board[axisX, axisY].PlayerColor = brain.CurrentPlayer;
                        gameState.CurrentMoveByBlack = true;
                        var opponentGameState = othelloDb.OthelloGamesStates.FirstOrDefault(gs =>
                            gs.OthelloGameId == id! && gs.Perspective == "player1");
                        if (opponentGameState != null)
                        {
                            opponentGameState.CurrentMoveByBlack = true;
                            othelloDb.OthelloGamesStates.Update(opponentGameState);
                        }
                        
                        (board, linesOfSquares) = valid.CheckValidMoves(brain, board, linesOfSquares);
                        ShowValidMoves();
                        TurnPieces();
                        
                       
                        SaveGameStates();
                        othelloDb.SaveChanges();
                        return movedone = true;
                    }

                }

            }
            return movedone = false;
            void TurnPieces()
            {
                for (var i = 0; i < linesOfSquares.Count; i++)
                {
                    for (var j = 0; j < linesOfSquares[i].Count; j++)
                    {
                        if (linesOfSquares[i][j].X == axisX && linesOfSquares[i][j].Y == axisY)
                        {
                            foreach (var item in linesOfSquares[i])
                            {
                                if (item.IsValid)
                                {
                                    board[item.X, item.Y].IsPlaced = true;
                                    board[item.X, item.Y].PlayerColor = brain.CurrentPlayer;
                                }

                                if (item.PlayerColor != brain.CurrentPlayer)
                                {
                                    board[item.X, item.Y].PlayerColor = brain.CurrentPlayer;
                                }
                            }
                        }
                    }
                }
            }
            void ShowValidMoves()
            {
                foreach (var firstDimension in linesOfSquares)
                {
                    foreach (var secondDimension in firstDimension)
                    {
                        if (secondDimension.IsValid)
                        {
                            board[secondDimension.X, secondDimension.Y].IsValid = true;
                        }
                    }
                }
            }

            void SaveGameStates()
            {
                brain.CurrentPlayer = gameState.CurrentMoveByBlack ? "Black" : "White";
                var serializedStr = brain.GetBrainJson();
                var gameStates = othelloDb.OthelloGamesStates.Where(og => og.OthelloGameId == id);

                foreach (var gState in gameStates)
                {
                    if (gState.Perspective == player)
                    {
                        gameState = gState;
                    }

                    gState.SerializedGameState = serializedStr;
                    othelloDb.OthelloGamesStates.Update(gState);
                }

                othelloDb.SaveChanges();
            }


        }
        
    }
    }
