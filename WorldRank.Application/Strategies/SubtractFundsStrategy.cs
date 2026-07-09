using WorldRank.Domain.Wallets;

namespace WorldRank.Application.Strategies
{
    public class SubtractFundsStrategy : IFundStrategy
    {
        public FundOperation Operation => FundOperation.Subtract;

        public void Execute(Wallet wallet, decimal amount) => wallet.Withdraw(amount);
    }
}
