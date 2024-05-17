namespace DigitalWallet.Features.Transactions.Common;
 
public class WalletOwnershipException : Exception
{
    private const string _message = "The user does not own one or both of the specified wallets.";

    public WalletOwnershipException() : base(_message)
    {
    }

    [DoesNotReturn]
    public static void Throw()
    {
        throw new WalletOwnershipException();
    }
}