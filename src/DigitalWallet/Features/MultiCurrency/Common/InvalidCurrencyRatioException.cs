namespace DigitalWallet.Features.MultiCurrency.Common;

public class InvalidCurrencyRatioException : Exception
{
    private const string _message = "Currency must have a non-zero ratio.";

    public InvalidCurrencyRatioException() : base(_message)
    {
    }

    [DoesNotReturn]
    public static void Throw()
    {
        throw new InvalidCurrencyRatioException();
    }
}