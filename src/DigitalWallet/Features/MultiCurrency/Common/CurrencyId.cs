namespace DigitalWallet.Features.MultiCurrency.Common;

public record CurrencyId(Guid Value)
{
    public static CurrencyId CreateUniqueId() => new CurrencyId(Guid.NewGuid());

    public static CurrencyId Create(Guid value) => new CurrencyId(value);

    public override string ToString()
    {
        return Value.ToString();
    }
};