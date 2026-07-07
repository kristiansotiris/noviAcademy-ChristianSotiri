using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank.Interfaces
{
    public interface IPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        IReadOnlyList<IWallet> Wallets { get; }
        void AddWallet(IWallet wallet);

    }
}
