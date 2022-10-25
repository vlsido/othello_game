namespace Domain
{
    public class OthelloGameState
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string SerializedGameState { get; set; } = default!;

        public int OthelloGameId { get; set; }
        public OthelloGame? OthelloGame { get; set; }
    }
}