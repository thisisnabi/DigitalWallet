namespace DigitalWallet.Features.UserWallet.Common;

public class Wallet
{
    public WalletId Id { get; private set; } = null!;

    public UserId UserId { get; private set; } = null!;

    public string Title { get; private set; } = null!;

    public decimal Balance { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public CurrencyId CurrencyId { get; private set; } = null!;

    public Currency Currency { get; private set; } = null!;

    public WalletStatus Status { get; private set; }

    public ICollection<Transaction> Transactions { get; private set; } = default!;

    public static Wallet Create(UserId userId, CurrencyId currencyId, string title)
    {
        return new Wallet
        {
            Id = WalletId.CreateUniqueId(),
            UserId = userId,
            CurrencyId = currencyId,
            Title = title,
            Balance = 0,
            CreatedOnUtc = DateTime.UtcNow,
            Status = WalletStatus.Active
        };
    }

    internal void IncreaseBalance(decimal amount)
    {
        Balance += amount;
    }

    internal void DecreaseBalance(decimal amount)
    {
        Balance -= amount;
    }

    internal void UpdateTitle(string title)
    {
        Title = title;
    }

    internal void Activate()
    {
        Status = WalletStatus.Active;
    }

    internal void Suspend()
    {
        Status = WalletStatus.Suspend;
    }

    private Wallet()
    {
        //EF
    }
}