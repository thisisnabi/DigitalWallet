namespace DigitalWallet.Features.MultiCurrency.Common;

public class DuplicateCurrencyException : Exception
{
    private const string _message = "{0} is duplicated.";

    public DuplicateCurrencyException(string code) : base(string.Format(_message, code))
    {
    }

    [DoesNotReturn]
    public static void Throw(string code)
    {
        throw new DuplicateCurrencyException(code);
    }
}