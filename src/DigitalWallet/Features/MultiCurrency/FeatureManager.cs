using ServiceCollector.Abstractions;

namespace DigitalWallet.Features.MultiCurrency;

public abstract class FeatureManager
{
    public const string EndpointTagName = "Currency";
    public const string Prefix = "/currencies";
    
    public class ServiceManager : IServiceDiscovery
    {
        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<CurrencyService>();
            serviceCollection.AddSingleton<CurrencyConverter>();
        }
    }
}

