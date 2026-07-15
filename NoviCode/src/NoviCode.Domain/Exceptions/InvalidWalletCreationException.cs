namespace NoviCode.Exceptions
{
    public sealed class InvalidWalletCreationException : WalletException
    {
        public InvalidWalletCreationException() : base("Invalid Request for creating Wallet.") { }
    }
}
