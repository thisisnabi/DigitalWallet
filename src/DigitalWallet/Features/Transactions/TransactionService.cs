
using DigitalWallet.Common.Persistence;
using DigitalWallet.Features.UserWallet;
using static DigitalWallet.Common.Persistence.WalletDbContextSchema;
using System.Data.Common;

namespace DigitalWallet.Features.Transactions;

public class TransactionService(
    WalletService walletService,
    WalletDbContext dbContext)
{
    private readonly WalletService _walletService = walletService;
    private readonly WalletDbContext _dbContext = dbContext;

    internal async Task DecrementBalanceAsync(Guid walletId, decimal amount, string description, CancellationToken ct)
    {
        if (!await _walletService.IsWalletAvailableAsync(walletId, ct))
        {
            throw new Exception($"Wallet `{walletId}` currently is not avialiablke.");
        }

        if (amount == 0)
        {
            throw new Exception($"you can't make a zero++ transaction.");
        }

        var dbTransaction = await _dbContext.Database.BeginTransactionAsync(ct);

        try
        {
            await _walletService.DecrementWalletBalanceAsync(walletId, amount, ct);

            var transaction = new Transaction
            {
                WalletId = walletId,
                Amount = amount,
                Kind = TransactionKind.Decremental,
                Type = TransactionType.User,
                Description = description,
                CreatedOn = DateTime.UtcNow,
            };

            await _dbContext.Transactions.AddAsync(transaction, ct);
            await _dbContext.SaveChangesAsync(ct);

            await dbTransaction.CommitAsync(ct);
        }
        catch (Exception)
        {
            await dbTransaction.RollbackAsync(ct);
        }
    }

    internal async Task IncrementBalanceAsync(Guid walletId, decimal amount, string description, CancellationToken ct)
    {
        if (!await _walletService.IsWalletAvailableAsync(walletId, ct))
        {
            throw new Exception($"Wallet `{walletId}` currently is not avialiablke.");
        }

        if (amount == 0)
        {
            throw new Exception($"you can't make a zero++ transaction.");
        }

        var dbTransaction = await _dbContext.Database.BeginTransactionAsync(ct);

        try
        {
            await _walletService.IncrementWalletBalanceAsync(walletId, amount, ct);

            var transaction = new Transaction
            {
                WalletId = walletId,
                Amount = amount,
                Kind = TransactionKind.Incremental,
                Type = TransactionType.User,
                Description = description,
                CreatedOn = DateTime.UtcNow,
            };

            await _dbContext.Transactions.AddAsync(transaction, ct);
            await _dbContext.SaveChangesAsync(ct);

            await dbTransaction.CommitAsync(ct);
        }
        catch (Exception)
        {
            await dbTransaction.RollbackAsync(ct);
        }
    }

    internal async Task WalletFundsAsync(Guid sourceWalletId, Guid destinationWalletId, decimal amount, string description, CancellationToken ct)
    {
        if (!await _walletService.IsWalletAvailableAsync(sourceWalletId, ct))
        {
            throw new Exception($"Wallet `{sourceWalletId}` currently is not avialiablke.");
        }
         
        if (!await _walletService.IsWalletAvailableAsync(destinationWalletId, ct))
        {
            throw new Exception($"Wallet `{destinationWalletId}` currently is not avialiablke.");
        }
         
        if (amount == 0)
        {
            throw new Exception($"you can't make a zero++ transaction.");
        }

        if (!await _walletService.IsUserOwnedAsync([sourceWalletId, destinationWalletId], ct))
        {
            throw new Exception($"exception message.");
        }

        var dbTransaction = await _dbContext.Database.BeginTransactionAsync(ct);

        try
        {
            var destinationAmount =  await _walletService.WalletFundsAsync(sourceWalletId, destinationWalletId, amount, ct);
            var date = DateTime.UtcNow;

            var transactionIncrement = new Transaction
            {
                WalletId = destinationWalletId,
                Amount = destinationAmount,
                Kind = TransactionKind.Incremental,
                Type = TransactionType.Funds,
                Description = description,
                CreatedOn = date,
            };

            var transactionDecrement = new Transaction
            {
                WalletId = sourceWalletId,
                Amount = amount,
                Kind = TransactionKind.Decremental,
                Type = TransactionType.Funds,
                Description = description,
                CreatedOn = date,
            };

            await _dbContext.Transactions.AddAsync(transactionIncrement, ct);
            await _dbContext.Transactions.AddAsync(transactionDecrement, ct);
            await _dbContext.SaveChangesAsync(ct);

            await dbTransaction.CommitAsync(ct);
        }
        catch (Exception)
        {
            await dbTransaction.RollbackAsync(ct);
        }
    }
}
