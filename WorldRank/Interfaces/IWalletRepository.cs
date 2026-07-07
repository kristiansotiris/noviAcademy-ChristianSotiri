using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Objects;

namespace WorldRank.Interfaces
{
    public interface IWalletRepository
    {
        void AddWallet(IWallet wallet, int playerId);
        IReadOnlyList<IWallet> GetWalletsByPlayer(int playerId);
    }
}
