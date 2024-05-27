namespace DigitalWallet.Common.Extensions;

internal static class IServiceCollectionExtensions
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

    public static IServiceCollection ConfigureValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

        return services;
    }
}
