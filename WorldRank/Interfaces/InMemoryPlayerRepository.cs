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

            player.Id = _nextId++;
            _players.Add(player);
        }

        public void DeletePlayer(int playerId)
        {
            IPlayer? player = _players.FirstOrDefault(p => p.Id == playerId);

            if (player == null)
            {
                Console.WriteLine("Player id not found");
                return;
            }

            _players.Remove(player);
        }

        public void FindPlayer(int playerId)
        {
            IPlayer? player = _players.FirstOrDefault(p => p.Id == playerId);

            if (player == null)
            {
                Console.WriteLine("Player not found");
                return;
            }

            Console.WriteLine($"User with id: {player.Id} Founded {player.Name}");
        }

        public void GroupPlayersByScore()
        {
            throw new NotImplementedException();
        }
    }
}
