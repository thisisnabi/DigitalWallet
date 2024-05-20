namespace DigitalWallet.Features.MultiCurrency;

public static class FeatureManager
{
    public const string EndpointTagName = "Currency";
    public const string Prefix = "/currencies";
    
    public static IServiceCollection ConfigureMultiCurrencyFeature(this IServiceCollection services)
    {
        services.AddScoped<CurrencyService>();
        services.AddSingleton<CurrencyConverter>();

        return services;
    }
}
