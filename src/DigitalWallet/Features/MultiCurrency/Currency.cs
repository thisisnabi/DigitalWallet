namespace DigitalWallet.Features.MultiCurrency;

public class Currency
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsBased { get; set; }

    public decimal RationToBase { get; set; }

    public DateTime LatestModifiedOnUtc { get; set; }

    
    public void UpdateRation(decimal ration)
    {
        if (IsBased)
        {
            throw new InvalidOperationException("Can't update ration for base.");
        }

        RationToBase = ration;
        LatestModifiedOnUtc = DateTime.UtcNow;  
    }
}
