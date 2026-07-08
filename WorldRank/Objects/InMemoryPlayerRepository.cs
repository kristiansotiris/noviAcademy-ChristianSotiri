using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Interfaces;
using Microsoft.Extensions.Logging;
namespace WorldRank.Objects
{
    public class InMemoryPlayerRepository(List<IPlayer> players, ILogger<InMemoryPlayerRepository> logger) : IPlayerRepository
    {
        private readonly ILogger<InMemoryPlayerRepository> _logger = logger;
        private List<IPlayer> _players = players;
        private int _nextId = 1;

        public void AddPlayer(IPlayer player)
        {
         
            if (player == null)
            {
                _logger.LogWarning("Attempted to add a null player.");
                return;
            }

            player.AssignId(_nextId++);
            _players.Add(player);
        }

        public void DeletePlayer(int playerId)
        {
            IPlayer? player = _players.FirstOrDefault(p => p.Id == playerId);

            if (player == null)
            {
                _logger.LogWarning($"Attempted to delete a player with ID {playerId}, but no such player exists.");
                return; 
            }

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
