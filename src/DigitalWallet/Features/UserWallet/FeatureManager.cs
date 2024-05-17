namespace DigitalWallet.Features.UserWallet;

public static class FeatureManager
{
    public const string EndpointTagName = "Wallet";


    public static IServiceCollection ConfigureWalletFeature(this IServiceCollection services)
    {
        services.AddScoped<WalletService>();

        return services;
    }

    public static IEndpointRouteBuilder MapWalletFeatures(this IEndpointRouteBuilder endpoint)
    {
        var groupEndpoint = endpoint.MapGroup("/wallets")
                                    .WithTags(EndpointTagName)
                                    .WithDescription("Provides endpoints related to wallet management.");

        groupEndpoint.AddCreateWalletEndpoint();
        groupEndpoint.AddChangeTitleEndpoint();
        groupEndpoint.AddSuspendWalletEndpoint();
        groupEndpoint.AddGetTransactionsEndpoint();
        groupEndpoint.AddGetBalanceEndpoint();
        groupEndpoint.AddActiveWalletEndpoint();

        return endpoint;
    }
}
