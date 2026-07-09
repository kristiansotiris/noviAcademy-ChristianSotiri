using WorldRank.Domain.Wallets;

namespace WorldRank.Application.Strategies
{
    public interface IFundStrategy
    {
        FundOperation Operation { get; }
        void Execute(Wallet wallet, decimal amount);
    }
}
