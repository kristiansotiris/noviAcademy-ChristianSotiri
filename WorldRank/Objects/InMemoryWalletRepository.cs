using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Interfaces;

namespace WorldRank.Objects
{
    public class InMemoryWalletRepository : IWalletRepository
    {
        public void AddWallet(Wallet wallet, int playerId)
        {
            throw new NotImplementedException();
        }

        public void GetWalletsByPlayer(int playerId)
        {
            throw new NotImplementedException();
        }
    }
}
