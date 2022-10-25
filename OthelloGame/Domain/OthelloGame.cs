using System.ComponentModel.DataAnnotations;
using OthelloGameBrain;

namespace Domain
{
    public class OthelloGame
    {
        public int Id { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.Now;
        public DateTime? GameOverAt { get; set; }
        public string? GameWonByPlayer { get; set; }

        [MaxLength(128)] 
        public string Player1Name { get; set; } = default!;
        public EPlayerType Player1Type { get; set; }

        [MaxLength(128)]
        public string Player2Name { get; set; } = default!;
        public EPlayerType Player2Type { get; set; }

        public int OthelloOptionId { get; set; }
        public OthelloOption? OthelloOption { get; set; }

        public ICollection<OthelloGameState>? OthelloGameStates { get; set; }
    }
}