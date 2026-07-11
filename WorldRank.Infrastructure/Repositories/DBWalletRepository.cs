using WorldRank.Domain.Exceptions;
using WorldRank.Infrastructure.Contexts;
using WorldRank.src.WorldRank.Application.Interfaces;
using WorldRank.src.WorldRank.Domain.Entities;
using WorldRank.src.WorldRank.Domain.Enums;
using WorldRank.src.WorldRank.Domain.Exceptions;

namespace WorldRank.Infrastructure.Repositories
{
    public class DBWalletRepository(WorldRankDbContext context) : IWalletRepository
    {
        private readonly WorldRankDbContext _context = context;

        public void Add(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            _context.SaveChanges();
        }

        public void Block(int playerId, Currency currency)
        {
            var wallet = GetWallet(playerId, currency);

            if (wallet != null)
            {
                wallet.Block();
                _context.SaveChanges();
            }
        }

        public void Deposit(int playerId, Currency currency, decimal amount)
        {
            var wallet = GetWallet(playerId, currency);
            if (wallet != null)
            {
                wallet.Deposit(amount);
                _context.SaveChanges();
            }

        }

        public Wallet[] GetAll()
        {
            return _context.Wallets.ToArray();
        }

        public List<Wallet> GetAllWalletsByPlayerId(int playerId)
        {
            List<Wallet> playerWallets = _context.Wallets.Where(w => w.PlayerId == playerId).ToList();

            if (playerWallets.Count == 0) throw new PlayerWalletsNotFound(playerId);

            return playerWallets;
        }

        public Wallet GetWallet(int playerId, Currency currency)
        {
            var wallet = _context.Wallets.FirstOrDefault(w => w.PlayerId == playerId && w.Currency == currency);

            if (wallet == null) throw new WalletNotFoundException(playerId, currency);

            return wallet;
        }

        public void Unblock(int playerId, Currency currency)
        {
            var wallet = GetWallet(playerId, currency);

            if (wallet != null)
            {
                wallet.Unblock();
                _context.SaveChanges();
            }

        }

        public void UpdateBalance(int playerId, Currency currency, decimal newBalance)
        {
            var wallet = GetWallet(playerId, currency);

            if (wallet != null)
            {
                wallet.SetBalance(newBalance);
                _context.SaveChanges();
            }

        }

        public void Withdraw(int playerId, Currency currency, decimal amount)
        {
            var wallet = GetWallet(playerId, currency);

            if (wallet != null)
            {
                wallet.Withdraw(amount);
                _context.SaveChanges();
            }
        }
    }
}
