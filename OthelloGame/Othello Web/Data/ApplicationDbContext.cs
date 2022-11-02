using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Othello_Web.Domain;

namespace Othello_Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<OthelloGame> OthelloGames { get; set; } = default!;
        public DbSet<OthelloGameState> OthelloGamesStates { get; set; } = default!;
        public DbSet<OthelloOption> OthelloOptions { get; set; } = default!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}