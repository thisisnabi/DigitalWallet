namespace DigitalWallet.Features.Transactions.Common;

public class Transaction
{
    public TransactionId Id { get; private set; } = null!;

    public WalletId WalletId { get; private set; } = null!;

    public Wallet Wallet { get; private set; } = null!;

    public string Description { get; private set; } = string.Empty;

    public decimal Amount { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public TransactionKind Kind { get; private set; }

    public TransactionType Type { get; private set; }

    public static Transaction CreateIncreaseUserTransaction(WalletId walletId, decimal amount, string description)
    {
        return new Transaction
        {
            Id = TransactionId.CreateUniqueId(),
            WalletId = walletId,
            Amount = amount,
            Kind = TransactionKind.Incremental,
            Type = TransactionType.User,
            Description = description,
            CreatedOnUtc = DateTime.UtcNow,
        };
    }

    public static Transaction CreateDecreaseUserTransaction(WalletId walletId, decimal amount, string description)
    {
        return new Transaction
        {
            Id = TransactionId.CreateUniqueId(),
            WalletId = walletId,
            Amount = amount,
            Kind = TransactionKind.Decremental,
            Type = TransactionType.User,
            Description = description,
            CreatedOnUtc = DateTime.UtcNow,
        };
    }

    public static Transaction CreateSourceFundsTransaction(WalletId sourceWalletId, decimal amount, string description, DateTime dateTime)
    {
        return new Transaction
        {
            Id = TransactionId.CreateUniqueId(),
            WalletId = sourceWalletId,
            Amount = amount,
            Kind = TransactionKind.Decremental,
            Type = TransactionType.Funds,
            Description = description,
            CreatedOnUtc = dateTime,
        };
    }

    public static Transaction CreateDestinationFundsTransaction(WalletId destinationWalletId, decimal amount, string description, DateTime dateTime)
    {
        return new Transaction
        {
            Id = TransactionId.CreateUniqueId(),
            WalletId = destinationWalletId,
            Amount = amount,
            Kind = TransactionKind.Incremental,
            Type = TransactionType.Funds,
            Description = description,
            CreatedOnUtc = dateTime,
        };
    }

    private Transaction()
    {
        //EF
    }
}