using System;
using WorldRank.Interfaces;

namespace WorldRank.Objects
{
    public class Player(string name, int score = 0) : IPlayer
    {
        private List<IWallet> _wallets = new List<IWallet>();
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public int Score { get; private set; } = score;
        int IPlayer.Score { get => Score; set => Score = value; }

        public IReadOnlyList<IWallet> Wallets => _wallets;

        public void AddWallet(IWallet wallet)
        {
            if (wallet == null) throw new ArgumentNullException(nameof(wallet));

            if (_wallets.Any(w => w.Currency == wallet.Currency)) throw new InvalidOperationException($"Player already has a {wallet.Currency} wallet.");

            _wallets.Add(wallet);
        }

    }
}
