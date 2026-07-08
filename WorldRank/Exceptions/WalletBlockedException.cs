using WorldRank.Enums;

namespace WorldRank.Exceptions
{
    public class WalletBlockedException : WalletException
    {
        public Currency WalletCurrency { get; }

        public WalletBlockedException(Currency currency) : base($"The wallet with currency {currency} is blocked and cannot perform this operation.")
        {
            WalletCurrency = currency;
        }

        public WalletBlockedException(string message, Exception inner) : base(message, inner) { }

    }
}
