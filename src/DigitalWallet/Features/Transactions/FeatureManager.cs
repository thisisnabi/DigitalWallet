namespace DigitalWallet.Features.Transactions;

public static class FeatureManager
{
    public const string EndpointTagName = "Transaction";
    public const string Prefix = "/transactions";


    public static IServiceCollection ConfigureTransactionFeature(this IServiceCollection services)
    {
        services.AddScoped<TransactionService>();

        return services;
    }
}
