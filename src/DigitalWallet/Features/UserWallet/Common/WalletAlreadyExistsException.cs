namespace DigitalWallet.Features.UserWallet.Common;

public class WalletAlreadyExistsException : Exception
{
    private const string _message = "A wallet already exists for user {o} and currency {1}.";

    public WalletAlreadyExistsException(UserId userId,CurrencyId currencyId) : base(string.Format(_message, userId, currencyId))
    {
    }

    [DoesNotReturn]
    public static void Throw(UserId userId, CurrencyId currencyId)
    {
        throw new WalletAlreadyExistsException(userId, currencyId);
    }
}