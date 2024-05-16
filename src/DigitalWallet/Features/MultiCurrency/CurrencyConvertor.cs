namespace DigitalWallet.Features.MultiCurrency;

public class CurrencyConvertor
{
    public decimal Convert(Currency sourceCurrency, Currency destinaitonCurrency, decimal amount) 
        => (sourceCurrency.Ratio / destinaitonCurrency.Ratio) * amount;
}
