using DigitalWallet.Common.Persistence;
using DigitalWallet.Features.MultiCurrency.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Features.MultiCurrency;

public class CurrencyService(WalletDbContext dbContext)
{
    private readonly WalletDbContext _dbContext = dbContext;
     
    public async Task<Currency> CreateAsync(CreateCurrency data, CancellationToken cancellationToken)
    {
        if (await _dbContext.Currencies.AnyAsync(x => x.Code == data.Code, cancellationToken))
        {
            // TODO: use specefif....
            throw new Exception($"{data.Code}Is duplicated.");
        }

        if (data.IsBased && data.RationToBase != 1)
        {
            // TODO: use specefif....
            throw new Exception($"Based currency must be have earion 1.");
        }
         
        var newCurrency = new Currency
        {
            Code = data.Code,
            IsBased = data.IsBased,
            LatestModifiedOnUtc = DateTime.UtcNow,
            RationToBase = data.RationToBase,
            Name = data.Name,
        };

        await _dbContext.Currencies.AddAsync(newCurrency, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return newCurrency;
    }

    public async Task UpdateRationAsync(UpdateRationCurrency data, CancellationToken cancellationToken)
    {
        if (data.Ration == 0)
        {
            // TODO: use specefif....
            throw new Exception("Ration must be more than zerrrrro!");
        }

        var currency = await _dbContext.Currencies.FirstOrDefaultAsync(x => x.Id == data.Id, cancellationToken);
        if (currency is null)
        {
            // TODO: use specefif....
            throw new Exception($"Currency is not valid with id {data.Id}.");
        }

        currency.UpdateRation(data.Ration);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

}
