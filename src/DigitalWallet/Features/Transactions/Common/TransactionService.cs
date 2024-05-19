using DigitalWallet.Features.UserWallet.Common;

namespace DigitalWallet.Features.Transactions.Common;

public class TransactionService(
    WalletService walletService,
    WalletDbContext dbContext)
{
    private readonly WalletService _walletService = walletService;
    private readonly WalletDbContext _dbContext = dbContext;

    internal async Task IncreaseBalanceAsync(WalletId walletId, decimal amount, string description, CancellationToken ct)
    {
        if (!await _walletService.IsWalletAvailableAsync(walletId, ct))
        {
            WalletUnavailableException.Throw(walletId);
        }

        InvalidTransactionAmountException.Throw(amount);

        var dbTransaction = await _dbContext.Database.BeginTransactionAsync(ct);

        try
        {
            await _walletService.IncreaseBalanceAsync(walletId, amount, ct);

            var transaction = Transaction.CreateIncreaseUserTransaction(walletId, amount, description);

            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync(ct);

            await dbTransaction.CommitAsync(ct);
        }
        catch (Exception)
        {
            await dbTransaction.RollbackAsync(ct);
        }
    }

    internal async Task DecreaseBalanceAsync(WalletId walletId, decimal amount, string description, CancellationToken ct)
    {
        if (!await _walletService.IsWalletAvailableAsync(walletId, ct))
        {
            WalletUnavailableException.Throw(walletId);
        }

        InvalidTransactionAmountException.Throw(amount);

        var dbTransaction = await _dbContext.Database.BeginTransactionAsync(ct);

        try
        {
            await _walletService.DecreaseBalanceAsync(walletId, amount, ct);

            var transaction = Transaction.CreateDecreaseUserTransaction(walletId, amount, description);

            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync(ct);

            await dbTransaction.CommitAsync(ct);
        }
        catch (Exception)
        {
            await dbTransaction.RollbackAsync(ct);
        }
    }

    internal async Task WalletFundsAsync(WalletId sourceWalletId, WalletId destinationWalletId, decimal amount, string description, CancellationToken ct)
    {
        if (!await _walletService.IsWalletAvailableAsync(sourceWalletId, ct))
        {
            WalletUnavailableException.Throw(sourceWalletId);
        }

        if (!await _walletService.IsWalletAvailableAsync(destinationWalletId, ct))
        {
            WalletUnavailableException.Throw(destinationWalletId);
        }

        InvalidTransactionAmountException.Throw(amount);

        if (!await _walletService.IsUserOwnedAsync([sourceWalletId, destinationWalletId], ct))
        {
            WalletOwnershipException.Throw();
        }

        var dbTransaction = await _dbContext.Database.BeginTransactionAsync(ct);

        try
        {
            var destinationAmount = await _walletService.WalletFundsAsync(sourceWalletId, destinationWalletId, amount, ct);

            var dateTime = DateTime.UtcNow;

            var transactionIncrement = Transaction.CreateDestinationFundsTransaction(
                destinationWalletId,
                destinationAmount,
                description,
                dateTime);

            var transactionDecrement = Transaction.CreateSourceFundsTransaction(
                sourceWalletId,
                amount,
                description,
                dateTime);

            _dbContext.Transactions.AddRange([transactionIncrement, transactionDecrement]);

            await _dbContext.SaveChangesAsync(ct);

            await dbTransaction.CommitAsync(ct);
        }
        catch (Exception)
        {
            await dbTransaction.RollbackAsync(ct);
        }
    }
}