using DigitalWallet.Common.Persistence;
using DigitalWallet.Features.MultiCurrency.Common;
using DigitalWallet.Features.MultiCurrency.GetAll;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.IntegrationTests;

public class GetAllCurrencies
{
    [Fact]
    public async Task GetAllCurrencies_ThereIsCurrencyInDatabase_ReturnListOfCurrencies()
    {
        // Arrange
        var expectedCurrencies = new GetCurrencyResponse()
        {
            Code = "rial",
            Name = "rial",
            Ratio = 1
        };

        const string inMemoryDatabaseName = "testDatabase";
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<WalletDbContextReadOnly>();
        dbContextOptionsBuilder.UseInMemoryDatabase(inMemoryDatabaseName);
        var walletDbContextReadOnly = new WalletDbContextReadOnly(dbContextOptionsBuilder.Options);

        var builder = new DbContextOptionsBuilder<WalletDbContext>();
        builder.UseInMemoryDatabase(inMemoryDatabaseName);
        var walletDbContext = new WalletDbContext(builder.Options);

        walletDbContext.Currencies.Add(Currency.Create("rial", "rial", 1));
        await walletDbContext.SaveChangesAsync();

        // Act
        var sut = await Endpoint.GetCurrencies(walletDbContextReadOnly, CancellationToken.None);

        // Assert
        sut.Should().OnlyContain(currenciesDto =>
            currenciesDto.Ratio == expectedCurrencies.Ratio &&
            currenciesDto.Code == expectedCurrencies.Code &&
            currenciesDto.Name == expectedCurrencies.Name &&
            !string.IsNullOrWhiteSpace(currenciesDto.Id));
    }
}