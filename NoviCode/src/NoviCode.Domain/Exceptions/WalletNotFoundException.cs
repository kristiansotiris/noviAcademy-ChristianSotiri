namespace NoviCode.Exceptions
{
    public sealed class WalletNotFoundException : WalletException
    {
        public WalletNotFoundException() : base("Wallet not found."){ }
    }
}
