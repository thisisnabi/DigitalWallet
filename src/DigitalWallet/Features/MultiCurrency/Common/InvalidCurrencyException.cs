namespace DigitalWallet.Features.MultiCurrency.Common;

public class InvalidCurrencyException : Exception
{
    private const string _message = "Currency with ID {0} is not valid!";

    public InvalidCurrencyException(CurrencyId currencyId) : base(string.Format(_message, currencyId))
    {
    }

    [DoesNotReturn]
    public static void Throw(CurrencyId currencyId)
    {
        throw new InvalidCurrencyException(currencyId);
    }
}