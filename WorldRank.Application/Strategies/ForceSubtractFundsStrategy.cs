using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Domain.Wallets;

namespace WorldRank.Application.Strategies
{
    public class ForceSubtractFundsStrategy : IFundStrategy
    {
        public FundOperation Operation => FundOperation.ForceSubtract;

        public void Execute(Wallet wallet, decimal amount) => wallet.ForceSubtract(amount);
    }
}
