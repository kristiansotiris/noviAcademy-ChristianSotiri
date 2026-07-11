using Microsoft.EntityFrameworkCore;
using WorldRank.src.WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Contexts
{
    public class WorldRankDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        public WorldRankDbContext(DbContextOptions<WorldRankDbContext> options) : base(options){ }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=worldrank.db");
            }
        }

    }
}
