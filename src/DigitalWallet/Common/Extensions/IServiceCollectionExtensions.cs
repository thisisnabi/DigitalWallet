namespace DigitalWallet.Common.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WalletDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(WalletDbContextSchema.DefaultConnectionStringName));
        });

        services.AddDbContext<WalletDbContextReadOnly>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(WalletDbContextSchema.DefaultReadOnlyConnectionStringName))
                   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        return services;
    }
}
