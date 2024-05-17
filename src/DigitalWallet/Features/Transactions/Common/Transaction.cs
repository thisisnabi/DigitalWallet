using static System.Runtime.InteropServices.JavaScript.JSType;

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


    public  static Transaction CreateIncreaseUserTransaction(WalletId walletId, decimal amount, string description)
    {
       return new Transaction
       {
           Id = TransactionId.CreateUniqueId(),
           WalletId = walletId,
           Amount = amount,
           Kind = TransactionKind.Incremental,
           Type = TransactionType.User,
           Description = description,
           CreatedOn = DateTime.UtcNow,
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
            CreatedOn = DateTime.UtcNow,
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
            CreatedOn = dateTime,
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
            CreatedOn = dateTime,
        };
    }
}
