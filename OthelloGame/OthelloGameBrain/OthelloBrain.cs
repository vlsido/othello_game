using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace OthelloGameBrain
{
    public class OthelloBrain
    {
        public static string BasePath = "D:\\othellogame\\OthelloGame\\SavedGames";
        public string CurrentPlayer = "Black";
        public GameBoard GameBoard;

        private readonly Random _rnd = new();

        public int BoardSizeHorizontal { get; set; } = 8;
        public int BoardSizeVertical { get; set; } = 8;

        public OthelloBrain(int boardSizeHorizontal, int boardSizeVertical)
        {
            GameBoard = new GameBoard
            {
                Board = new BoardSquareState[boardSizeHorizontal, boardSizeVertical]
            };
        }

        public BoardSquareState[,] GetBoard()
        {
            return CreateBoard(GameBoard.Board);
        }

        private BoardSquareState[,] CreateBoard(BoardSquareState[,] board)
        {
            var res = new BoardSquareState[board.GetLength(0), board.GetLength(1)];
            for (var x = 0; x < board.GetLength(0); x++)
            for (var y = 0; y < board.GetLength(1); y++)
            {
                board[x, 0].IsFileNotation = true;
                res[x, y] = board[x, y];
            }
                
            return res;
        }

        //public string GetBrainJson(OthelloBrain brain, BoardSquareState[,] board, ApplicationDbContext db)
        public string GetBrainJson(OthelloBrain brain, BoardSquareState[,] board)
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                AllowTrailingCommas = true
            };

            var dto = new GameDTO();

            for (var x = 0; x <= board.GetLength(0); x++)
            {
                dto.GameBoard.Board.Add(new List<BoardSquareState>());
                for (var y = 0; y < board.GetLength(1); y++)
                {
                    dto.GameBoard.Board[x].Add(new BoardSquareState());
                }
                

            }
            dto.CurrentPlayer = brain.CurrentPlayer;

            for (var x = 0; x < board.GetLength(0); x++)
            {
                for (var y = 0; y < board.GetLength(1); y++)
                {
                    dto.GameBoard.Board[x][y] = board[x, y];
                }
            }

            dto.BoardSizeHorizontal = brain.BoardSizeHorizontal;
            dto.BoardSizeVertical = brain.BoardSizeVertical;

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
                boardSizeHorizontal = dto.GameBoard.Board.Count;
                BoardSizeVertical = dto.GameBoard.Board.Count;
            }
            else
            {
                Console.WriteLine("Whoops, something went wrong...");
                return (brain, board)!;
            }

            brain.GameBoard.Board = new BoardSquareState[boardSizeHorizontal, boardSizeVertical];

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

        //public static GameDTO? LoadGame(string name)
        //{
        //    var savedBoardState = BasePath + name + Path.DirectorySeparatorChar + "boardState.json";
        //    if (File.Exists(savedBoardState))
        //    {
        //        string jsonStringBoard = File.ReadAllText(savedBoardState);
        //        return JsonSerializer.Deserialize<GameDTO>(jsonStringBoard)
        //    }
        //    else
        //    {
        //        var db = new ApplicationDbContext();
        //    }
        //}
    }
    }
