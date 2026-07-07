using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank.Interfaces
{
    public class InMemoryPlayerRepository(List<IPlayer> players) : IPlayerRepository
    {
        private List<IPlayer> _players = players;
        private int _nextId = 1;

        public void AddPlayer(IPlayer player)
        {

            if (player == null)
            {
                throw new ArgumentNullException(nameof(player), "Player cannot be null.");
            }

            player.AssignId(_nextId++);
            _players.Add(player);
        }

        public void DeletePlayer(int playerId)
        {
            IPlayer? player = _players.FirstOrDefault(p => p.Id == playerId);

            if (player == null)
                throw new InvalidOperationException($"Player with id: {playerId} is not found.");

            _players.Remove(player);
        }

        public IPlayer? FindPlayer(int playerId)
        {
            return _players.FirstOrDefault(p => p.Id == playerId);
        }

        public IReadOnlyDictionary<int, List<IPlayer>> GroupPlayersByScore()
        {
            return _players.GroupBy(p => p.Score).ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}
