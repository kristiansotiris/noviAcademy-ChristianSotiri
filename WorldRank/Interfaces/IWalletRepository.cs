using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Objects;

namespace WorldRank.Interfaces
{
    public interface IWalletRepository
    {
        void AddWallet(Wallet wallet, int playerId);
        void GetWalletsByPlayer(int playerId);
    }
}
