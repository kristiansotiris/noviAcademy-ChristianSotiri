using Microsoft.Extensions.Logging;
using WorldRank.Interfaces;

namespace WorldRank.Objects
{
    public class InMemoryWalletRepository(List<IPlayer> players, ILogger<InMemoryWalletRepository> logger) : IWalletRepository
    {
        private  List<IPlayer> _players = players;
        private readonly ILogger<InMemoryWalletRepository> _logger = logger;

        public void AddWallet(IWallet wallet, int playerId)
        {
            if (wallet == null)
            {
                _logger.LogWarning("Attempted to add a null wallet for player with id: {PlayerId}.", playerId);
                return;
            }


            IPlayer? player = _players.FirstOrDefault(p => p.Id == playerId);

            if (player == null)
            {
                _logger.LogWarning("Player with id: {PlayerId} is not found. Cannot add wallet.", playerId);
                return; 
            }

            player.AddWallet(wallet);
        }

        public IReadOnlyList<IWallet> GetWalletsByPlayer(int playerId)
        {
            IPlayer? player = _players.FirstOrDefault(p => p.Id == playerId);

            if (player == null)
            {
                _logger.LogWarning("Player Wallet with id : {playerId} is not found.", playerId);
                return Array.Empty<IWallet>();
            }

            return player.Wallets;
        }
    }
}
