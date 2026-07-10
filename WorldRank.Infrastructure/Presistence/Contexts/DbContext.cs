using Microsoft.EntityFrameworkCore;
using WorldRank.Console;
using WorldRank.Domain.Wallets;

namespace WorldRank.Infrastructure.Presistence.Contexts
{
    public partial class DbContext(DbContextOptions<DbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Wallet> Wallets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Players");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Score).IsRequired();
            });


            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallets");
                entity.HasKey(e => e.PlayerId);
                entity.Property(e => e.Balance).IsRequired();
                entity.Property(e => e.PlayerId).IsRequired();
                entity.Property(e => e.Currency).IsRequired();
                entity.Property(e => e.IsBlocked).IsRequired();
                entity.HasOne<Player>()
                      .WithMany()
                      .HasForeignKey(e => e.PlayerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
