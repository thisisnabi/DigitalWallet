using DigitalWallet.Features.UserWallet;

namespace DigitalWallet.Features.Transactions;

public class Transaction
{
    public Guid Id { get; set; }

    public Guid WalletId { get; set; }
    public Wallet Wallet { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedOn { get; set; }

    public TransactionKind Kind { get; set; }

    public TransactionType Type { get; set; }
}
