using Microsoft.EntityFrameworkCore;
using WorldRank.src.WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Contexts
{
    public class WorldRankDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        public WorldRankDbContext(DbContextOptions<WorldRankDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Score);
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Id).ValueGeneratedOnAdd();
                entity.Property(w => w.PlayerId).IsRequired();
                entity.Property(w => w.Currency).HasConversion<string>();
                entity.Property(w => w.Balance).HasColumnType("decimal(18,2)");
                entity.Property(w => w.IsBlocked);

                entity.HasIndex(w => new { w.PlayerId, w.Currency }).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}