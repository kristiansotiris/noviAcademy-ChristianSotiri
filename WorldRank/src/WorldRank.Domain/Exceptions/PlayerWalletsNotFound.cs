using WorldRank.src.WorldRank.Domain.Entities;
using WorldRank.src.WorldRank.Domain.Enums;
using WorldRank.src.WorldRank.Domain.Exceptions;

namespace WorldRank.Domain.Exceptions
{
    public sealed class PlayerWalletsNotFound : WalletException
    {
        public int PlayerId { get; }
        public Currency Currency { get; }

        public PlayerWalletsNotFound(Currency currency, int playerId) : base($"No {currency} wallet found for player {playerId}.")
        {
            PlayerId = playerId;
            Currency = currency;
        }

        public PlayerWalletsNotFound(int playerId) : base($"No wallet found for player {playerId}.")
        {
            PlayerId = playerId;
        }

        public PlayerWalletsNotFound(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
