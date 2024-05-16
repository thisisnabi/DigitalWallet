using DigitalWallet.Common.Persistence;
using DigitalWallet.Features.MultiCurrency;
using DigitalWallet.Features.UserWallet.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Features.UserWallet;

public class WalletService(CurrencyService currencyService, WalletDbContext dbContext)
{
    private readonly CurrencyService _currencyService = currencyService;
    private readonly WalletDbContext _dbContext = dbContext;


    internal async Task<Guid> CreateAsync(CreateUserWallet data, CancellationToken ct)
    {
        if (!await _currencyService.HasByIdAsync(data.CurrencyId, ct))
        {
            throw new Exception($"Currency with id {data.CurrencyId} is not available!");
        }

        if (await _dbContext.Wallets.AnyAsync(x => x.UserId == data.UserId && x.CurrencyId == data.CurrencyId, ct))
        {
            throw new Exception($"you have waller for this currency {data.CurrencyId}.");
        }


        var wallet = new Wallet
        {
            Balance = 0,
            CreatedOn = DateTime.UtcNow,
            CurrencyId = data.CurrencyId,
            Status = WalletStatus.Active,
            Title = data.Title,
            UserId = data.UserId
        };

        await _dbContext.Wallets.AddAsync(wallet, ct);
        await _dbContext.SaveChangesAsync(ct);

        return wallet.Id;
    }

    internal async Task SuspendAsync(Guid id, CancellationToken ct)
    {
        var wallet = await FetchWalletAsync(id, ct);

        wallet.Status = WalletStatus.Suspend;
        await _dbContext.SaveChangesAsync(ct);
    }

    internal async Task ChangeTitleAsync(Guid id, string title, CancellationToken ct)
    {
        var wallet = await FetchWalletAsync(id, ct);

        wallet.Title = title;
        await _dbContext.SaveChangesAsync(ct);
    }

    private async Task<Wallet> FetchWalletAsync(Guid Id, CancellationToken ct)
    {
        var wallet = await _dbContext.Wallets.Include(x => x.Currency).FirstOrDefaultAsync(x => x.Id == Id, ct);

        if (wallet is null)
        {
            throw new Exception("Invalid wallet id!");
        }

        return wallet;
    }

    internal async Task<bool> IsWalletAvailableAsync(Guid walletId, CancellationToken ct)
    {
        var wallet = await FetchWalletAsync(walletId, ct);

        return wallet.Status == WalletStatus.Active;


    }

    internal async Task IncrementWalletBalanceAsync(Guid walletId, decimal amount, CancellationToken ct)
    {
        var wallet = await FetchWalletAsync(walletId, ct);

        wallet.Balance += amount;
        await _dbContext.SaveChangesAsync(ct);
    }

    internal async Task DecrementWalletBalanceAsync(Guid walletId, decimal amount, CancellationToken ct)
    {
        var wallet = await FetchWalletAsync(walletId, ct);

        if ((wallet.Balance - amount) < 0)
        {
            throw new Exception("Wallet balance is not valid.");
        }

        wallet.Balance -= amount;
        await _dbContext.SaveChangesAsync(ct);
    }

    internal async Task<bool> IsUserOwnedAsync(List<Guid> WalletIds, CancellationToken ct)
    {
        var wallets = await _dbContext.Wallets.Where(x => WalletIds.Contains(x.Id))
                                              .ToListAsync(ct);

        return wallets.Select(x => x.UserId).Distinct().Count() == 1;
    }

    internal async Task<decimal> WalletFundsAsync(Guid sourceWalletId, Guid destinationWalletId, decimal amount, CancellationToken ct)
    {
        var walletSource = await FetchWalletAsync(sourceWalletId, ct);
        var walletDestination = await FetchWalletAsync(destinationWalletId, ct);

        walletSource.Balance -= amount;

        var destinationAmount = (walletSource.Currency.RationToBase / walletDestination.Currency.RationToBase) * amount;
        walletDestination.Balance += destinationAmount;

        await _dbContext.SaveChangesAsync(ct);

        return destinationAmount;
    }



}
