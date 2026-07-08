namespace WorldRank.Exceptions
{
    public class InsufficientFundsException : WalletException
    {
        public decimal RequestedAmount { get; }
        public decimal AvailableBalance { get; }

        public InsufficientFundsException(decimal amount, decimal balance)
        {
            RequestedAmount = amount;
            AvailableBalance = balance;
        }

        public InsufficientFundsException(string message) : base(message) { }

        public InsufficientFundsException(string message, Exception inner) : base(message, inner) { }

    }
}
