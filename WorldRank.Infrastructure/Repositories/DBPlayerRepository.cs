using WorldRank.Infrastructure.Contexts;
using WorldRank.src.WorldRank.Application.Interfaces;
using WorldRank.src.WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Repositories
{
    public class DBPlayerRepository : IPlayerRepository
    {
        private readonly WorldRankDbContext _context;
        public DBPlayerRepository(WorldRankDbContext context)
        {
            _context = context;
        }

        public void AddPlayer(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
        }

        public void DeletePlayer(int playerId)
        {
            var player = _context.Players.Find(playerId);
            if (player != null)
            {
                _context.Remove(player);
                _context.SaveChanges();
            }
        }

        public Player? FindPlayer(int playerId)
        {
            return _context.Players.Find(playerId);
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _context.Players.ToList();
        }

        public IEnumerable<IGrouping<int, Player>> GroupPlayersByScore()
        {
            return _context.Players.ToList().GroupBy(item => item.Score);
        }
    }
}
