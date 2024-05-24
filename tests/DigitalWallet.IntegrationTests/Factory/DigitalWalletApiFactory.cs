using DigitalWallet.Common.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalWallet.IntegrationTests.Factory;

public class DigitalWalletApiFactory : WebApplicationFactory<IAssemblyMarker>, IAsyncLifetime
{
          

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Clear default db context 
        builder.ConfigureServices(services =>
        {
            ServiceDescriptor? descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<WalletDbContext>));

            if(descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<WalletDbContext>(options =>
            {
                options.UseInMemoryDatabase("WalletDbContextTest");
            });


            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var testDbContext = scopedServices.GetRequiredService<WalletDbContext>();

                try
                {
                    testDbContext.Database.EnsureCreated();
                    if (!testDbContext.Database.IsInMemory())
                    {
                        testDbContext.Database.Migrate();
                    }
                    //db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred applying migrations to the database. Error: {Message}", ex.Message);
                }
            }
        });

        
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
