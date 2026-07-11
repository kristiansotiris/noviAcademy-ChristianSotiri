using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace WorldRank.Infrastructure.Contexts
{
    public class WorldRankDbContextFactory : IDesignTimeDbContextFactory<WorldRankDbContext>
    {
        public WorldRankDbContext CreateDbContext(string[] args)
        {

            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "WorldRank.Console"); // finds the path

            var configuration = new ConfigurationBuilder()
             .SetBasePath(basePath)
             .AddJsonFile("appsettings.json")
             .Build();

             var connectionString = configuration.GetConnectionString("WorldRankDb"); // gets the connection string

            var optionsBuilder = new DbContextOptionsBuilder<WorldRankDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new WorldRankDbContext(optionsBuilder.Options);
        }
    }
}
