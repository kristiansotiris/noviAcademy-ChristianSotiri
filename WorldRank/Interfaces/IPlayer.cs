using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank.Interfaces
{
    public interface IPlayer
    {
        public int Id { get;  }
        public string Name { get; }
        public int Score { get;}
        IReadOnlyList<IWallet> Wallets { get; }
        void AssignId(int id);
        void AddScore(int points);
        void AddWallet(IWallet wallet);
    }
}
