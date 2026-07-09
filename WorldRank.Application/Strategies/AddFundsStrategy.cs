
using WorldRank.Domain.Wallets;

namespace WorldRank.Application.Strategies
{
    public class AddFundsStrategy : IFundStrategy
    {
        public FundOperation Operation => FundOperation.Add;

        public void Execute(Wallet wallet, decimal amount) => wallet.Deposit(amount);
    }
}
