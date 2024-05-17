namespace DigitalWallet.Features.MultiCurrency.GetAll;

public static class Endpoint
{
    public static IEndpointRouteBuilder AddGetAllEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/",
            async (WalletDbContextReadOnly _dbContext, CancellationToken cancellationToken) =>
            {
                var currencies = await _dbContext.GetCurrencies()
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

        return endpoint;
    }
}
