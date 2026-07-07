using WorldRank.Enums;
using WorldRank.Interfaces;

namespace WorldRank.Objects
{
    public class Player(string name, int score = 0) : IPlayer
    {
        private Dictionary<Currency, IWallet> _wallets = new Dictionary<Currency, IWallet>();
        public int Id { get; private set; }
        public string Name { get; private set; } = name;
        public int Score { get; private set; } = score;

        public IReadOnlyList<IWallet> Wallets => _wallets.Values.ToList();

        public void AddScore(int points)
        {
            if (points < 0) throw new ArgumentException(nameof(points));

            Score += points;
        }

        public void AddWallet(IWallet wallet)
        {
            if (wallet == null) throw new ArgumentNullException(nameof(wallet));

            if (_wallets.ContainsKey(wallet.Currency))
                throw new InvalidOperationException($"Player already has a {wallet.Currency} wallet.");

            _wallets.Add(wallet.Currency, wallet);
        }

        public void AssignId(int id)
        {

            Id = id;
        }
    }
}
