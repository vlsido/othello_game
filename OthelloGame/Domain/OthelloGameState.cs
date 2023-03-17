using Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class OthelloGameState
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string SerializedGameState { get; set; } = default!;
        public string? Perspective { get; set; }
        public int AxisX { get; set; }
        public int AxisY { get; set; }
        public int BlackScore { get; set; }
        public int WhiteScore { get; set; }
        public bool CurrentMoveByBlack { get; set; } = true;
        public string? Winner { get; set; }

        public int OthelloGameId { get; set; }
        public OthelloGame? OthelloGame { get; set; }
    }
}