namespace DigitalWallet.Features.MultiCurrency.Common;

public class Currency
{
    public CurrencyId Id { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Ratio { get; set; }

    public DateTime ModifiedOnUtc { get; set; }

    public ICollection<Wallet> Wallets { get; set; }

    public static Currency Create(string code, string name, decimal ratio)
        => new Currency
        {
            Id = CurrencyId.CreateUniqueId(),
            Ratio = ratio,
            Code = code,
            Name = name,
            ModifiedOnUtc = DateTime.UtcNow
        };

    public void UpdateRation(decimal ratio)
    {
        Ratio = ratio;
        ModifiedOnUtc = DateTime.UtcNow;
    }
}
