using Carter;

namespace DigitalWallet.Features.MultiCurrency.GetAll;

public  class Endpoint : ICarterModule 
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup(FeatureManager.Prefix)
            .WithTags(FeatureManager.EndpointTagName)
            .MapGet("/",
            async (WalletDbContextReadOnly dbContext, CancellationToken cancellationToken) =>
            {
                var currencies = await dbContext.GetCurrencies()
                    .OrderByDescending(x => x.Name)
                    .Select(x => new
                    {
                        Id = x.Id.ToString(),
                        x.Name,
                        x.Code,
                        x.Ratio
                    })
                    .ToListAsync(cancellationToken);

                return Results.Ok(currencies);
            });

    }
}
