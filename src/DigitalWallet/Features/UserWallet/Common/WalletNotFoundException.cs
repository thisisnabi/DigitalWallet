namespace DigitalWallet.Features.UserWallet.Common;
  
public class WalletNotFoundException : Exception
{
    private const string _message = "Wallet with ID {0} not found.";

    public WalletNotFoundException(WalletId walletId) : base(string.Format(_message, walletId))
    {
    }

    [DoesNotReturn]
    public static void Throw(WalletId walletId)
    {
        throw new WalletNotFoundException(walletId);
    }
}