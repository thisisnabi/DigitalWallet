using DigitalWallet.Features.MultiCurrency;
using DigitalWallet.Features.Transactions;

namespace DigitalWallet.Features.UserWallet;

public class Wallet
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public decimal Balance { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CurrencyId { get; set; }

    public Currency Currency { get; set; } = null!;

    public WalletStatus Status { get; set; }

    public ICollection<Transaction> Transactions { get; set; }
}


