namespace NoviCode.Exceptions
{
    public sealed class WalletNotFoundException : WalletException
    {
        public Guid Id { get; }
        public WalletNotFoundException(Guid id) : base("Wallet not found.")
        {
            Id = id;
        }
    }
}
