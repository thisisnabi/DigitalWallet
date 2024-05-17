namespace DigitalWallet.Features.Transactions.Common;

public class Transaction
{
    public TransactionId Id { get; set; } = null!;

    public WalletId WalletId { get; set; } = null!;

    public Wallet Wallet { get; set; } = null!;

    public required string Description { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedOn { get; set; }

    public TransactionKind Kind { get; set; }

    public TransactionType Type { get; set; }
}
