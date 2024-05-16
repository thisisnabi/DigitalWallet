namespace DigitalWallet.Features.MultiCurrency;

public class Currency
{
    public CurrencyId Id { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;
 
    public decimal Ratio { get; set; }

    public DateTime ModifiedOnUtc { get; set; }
 
    public void UpdateRation(decimal ratio)
    {
        Ratio = ratio;
        ModifiedOnUtc = DateTime.UtcNow;
    }
}
