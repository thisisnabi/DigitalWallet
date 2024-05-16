namespace DigitalWallet.Features.MultiCurrency.Common;

public class CurrencyConverter
{
    public decimal Convert(Currency sourceCurrency, Currency destinaitonCurrency, decimal amount)
        => sourceCurrency.Ratio / destinaitonCurrency.Ratio * amount;
}
