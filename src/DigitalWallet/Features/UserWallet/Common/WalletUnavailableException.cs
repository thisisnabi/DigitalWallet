namespace DigitalWallet.Features.UserWallet.Common;

  
public class WalletUnavailableException : Exception
{
    private const string _message = "Wallet with ID `{0}` is currently unavailable.";

    public WalletUnavailableException(WalletId walletId) : base(string.Format(_message, walletId))
    {
    }

    [DoesNotReturn]
    public static void Throw(WalletId walletId)
    {
        throw new WalletUnavailableException(walletId);
    }
}