namespace DigitalWallet.Features.UserWallet.Common;

public class WalletService(CurrencyService currencyService, WalletDbContext dbContext)
{
    private readonly CurrencyService _currencyService = currencyService;
    private readonly WalletDbContext _dbContext = dbContext;

    public async Task<WalletId> CreateAsync(UserId userId, CurrencyId currencyId, string title, CancellationToken cancellationToken)
    {
        if (!await _currencyService.IsCurrencyIdValidAsync(currencyId, cancellationToken))
        {
            InvalidCurrencyException.Throw(currencyId);
        }

        if (await _dbContext.Wallets.AnyAsync(x => x.UserId == userId && x.CurrencyId == currencyId, cancellationToken))
        {
            WalletAlreadyExistsException.Throw(userId, currencyId);
        }

        var wallet = Wallet.Create(userId, currencyId, title);

        await _dbContext.Wallets.AddAsync(wallet, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return wallet.Id;
    }

    private async Task<Wallet> GetWalletAsync(WalletId walletId, CancellationToken cancellationToken)
    {
        var wallet = await _dbContext.Wallets
                                     .Include(x => x.Currency)
                                     .FirstOrDefaultAsync(x => x.Id == walletId, cancellationToken);

        if (wallet is null)
        {
            WalletNotFoundException.Throw(walletId);
        }

        return wallet;
    } 

    internal async Task SuspendAsync(WalletId walletId, CancellationToken  cancellationToken)
    {
        var wallet = await GetWalletAsync(walletId, cancellationToken);

        wallet.Status = WalletStatus.Suspend;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }


    internal async Task ActiveAsync(WalletId walletId, CancellationToken cancellationToken)
    {
        var wallet = await GetWalletAsync(walletId, cancellationToken);

        wallet.Status = WalletStatus.Active;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    internal async Task ChangeTitleAsync(WalletId walletId, string title, CancellationToken cancellationToken)
    {
        var wallet = await GetWalletAsync(walletId, cancellationToken);

        wallet.Title = title;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
     
    internal async Task<bool> IsWalletAvailableAsync(WalletId walletId, CancellationToken cancellationToken)
    {
        var wallet = await GetWalletAsync(walletId, cancellationToken);
        return wallet.Status == WalletStatus.Active;
    }

    internal async Task IncreaseBalanceAsync(WalletId walletId, decimal amount, CancellationToken cancellationToken)
    {
        var wallet = await GetWalletAsync(walletId, cancellationToken);

        wallet.Balance += amount;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    internal async Task DecreaseBalanceAsync(WalletId walletId, decimal amount, CancellationToken cancellationToken)
    {
        var wallet = await GetWalletAsync(walletId, cancellationToken);

        if (wallet.Balance - amount < 0)
        {
            InsufficientBalanceException.Throw();
        }

        wallet.Balance -= amount;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    internal async Task<bool> IsUserOwnedAsync(List<WalletId> WalletIds, CancellationToken ct)
    {
        return (await _dbContext.Wallets.Where(x => WalletIds.Contains(x.Id))
                                        .ToListAsync(ct))
                                        .DistinctBy(x => x.UserId)
                                        .Count() == 1;
    }

    internal async Task<decimal> WalletFundsAsync(WalletId sourceWalletId, WalletId destinationWalletId, decimal amount, CancellationToken cancellationToken)
    {
        var walletSource = await GetWalletAsync(sourceWalletId, cancellationToken);
        var walletDestination = await GetWalletAsync(destinationWalletId, cancellationToken);

        walletSource.Balance -= amount;

        var destinationAmount = walletSource.Currency.Ratio / walletDestination.Currency.Ratio * amount;
        walletDestination.Balance += destinationAmount;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return destinationAmount;
    }
}
