namespace DigitalWallet.Features.UserWallet;

public static class FeatureManager
{
    public const string EndpointTagName = "Wallet";
    public const string Prefix = "/wallets";
    
    public static IServiceCollection ConfigureWalletFeature(this IServiceCollection services)
    {
        services.AddScoped<WalletService>();

        return services;
    }
}
