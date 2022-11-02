using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<OthelloGame> OthelloGames { get; set; } = default!;
        public DbSet<OthelloGameState> OthelloGamesStates { get; set; } = default!;
        public DbSet<OthelloOption> OthelloOptions { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<OthelloGame>().Property(g => g.Player1Name).HasMaxLength(128);
        }
    }
}