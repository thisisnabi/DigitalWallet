using ServiceCollector.Abstractions;

namespace DigitalWallet.Features.UserWallet;

public abstract class FeatureManager
{
    public const string EndpointTagName = "Wallet";
    public const string Prefix = "/wallets";

    public class ServiceManager : IServiceDiscovery
    {
        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<WalletService>();
        }
    }
}