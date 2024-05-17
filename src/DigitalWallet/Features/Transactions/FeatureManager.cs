using DigitalWallet.Features.Transactions.DecreaseWalletBalance;
using DigitalWallet.Features.Transactions.IncreaseWalletBalance;

namespace DigitalWallet.Features.Transactions;

public static class FeatureManager
{
    public const string EndpointTagName = "Transaction";


    public static IServiceCollection ConfigureTransactionFeature(this IServiceCollection services)
    {
        services.AddScoped<TransactionService>();

        return services;
    }

    public static IEndpointRouteBuilder MapTransactionFeatures(this IEndpointRouteBuilder endpoint)
    {
        var groupEndpoint = endpoint.MapGroup("/transactions")
                                    .WithTags(EndpointTagName)
                                    .WithDescription("Provides endpoints related to Transactions management.");

        groupEndpoint.AddIncreaseWalletBalanceEndPoint();
        groupEndpoint.AddDecreaseWalletBalanceEndPoint();
        groupEndpoint.AddGetWalletTransactionsEndPoint();
        groupEndpoint.AddWalletFundsEndPoint();

        return endpoint;
    }
}
