namespace DigitalWallet.Features.UserWallet.Common;

public class Wallet
{
    public WalletId Id { get; set; } = null!;

    public UserId UserId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public decimal Balance { get; set; }

    public DateTime CreatedOn { get; set; }

    public CurrencyId CurrencyId { get; set; } = null!;

    public Currency Currency { get; set; } = null!;

    public WalletStatus Status { get; set; }

    public ICollection<Transaction> Transactions { get; set; }

    public static Wallet Create(UserId userId, CurrencyId currencyId, string title)
    {
        return new Wallet
        {
            Id = WalletId.CreateUniqueId(),
            UserId = userId,
            CurrencyId = currencyId,
            Title = title,
            Balance = 0,
            CreatedOn = DateTime.UtcNow,
            Status = WalletStatus.Active
        };
    }
}


