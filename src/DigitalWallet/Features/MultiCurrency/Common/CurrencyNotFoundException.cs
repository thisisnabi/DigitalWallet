namespace DigitalWallet.Features.MultiCurrency.Common;

public class CurrencyNotFoundException : Exception
{
    private const string _message = "Currency with ID {0} not found.";

    public CurrencyNotFoundException(CurrencyId currencyId) : base(string.Format(_message, currencyId))
    {
    }

    public CurrencyNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    [DoesNotReturn]
    public static void Throw(CurrencyId currencyId)
    {
        throw new CurrencyNotFoundException(currencyId);
    }
}