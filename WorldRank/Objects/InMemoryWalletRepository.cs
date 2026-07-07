using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Enums;
using WorldRank.Interfaces;

namespace WorldRank.Objects
{
    public class InMemoryWalletRepository(List<IPlayer> players) : IWalletRepository
    {
        private  List<IPlayer> _players = players;

        public void AddWallet(IWallet wallet, int playerId)
        {
            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            IPlayer? player = _players.FirstOrDefault(p => p.Id == playerId);

            if (player == null)
                throw new InvalidOperationException($"Player with id: {playerId} is not found.");

            player.AddWallet(wallet);
        }

        public IReadOnlyList<IWallet> GetWalletsByPlayer(int playerId)
        {
            IPlayer? player = _players.FirstOrDefault(p => p.Id == playerId);

            if(player == null) throw new InvalidOperationException($"Player with id: {playerId} is not found.");

            return player.Wallets;
        }
    }
}
