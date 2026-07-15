namespace NoviCode;

// Base type for every wallet-related domain exception.
public abstract class WalletException : Exception
{
    public WalletException(string? message) : base(message)
    {
    }

    protected WalletException(string message, Exception innerException) : base(message)
	{
	}
}
