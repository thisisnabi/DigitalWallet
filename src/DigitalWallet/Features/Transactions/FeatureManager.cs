using ServiceCollector.Abstractions;

namespace DigitalWallet.Features.Transactions;

public abstract class FeatureManager
{
    public const string EndpointTagName = "Transaction";
    public const string Prefix = "/transactions";
    
    public class ServiceManager : IServiceDiscovery
    {
        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<TransactionService>();
        }
    }
}