using Microsoft.EntityFrameworkCore;
using WorldRank.src.WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Contexts
{
    public class WorldRankDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
