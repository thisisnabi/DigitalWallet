 namespace DigitalWallet.Features.UserWallet.GetTransactions;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddGetTransactionsEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/{wallet-id:guid:required}/transactions/",
            async ([FromRoute(Name = "wallet-id")]Guid Id, WalletDbContextReadOnly _dbContext, CancellationToken cancellationToken) =>
        {

            var walletId = WalletId.Create(Id);

            var transactions = await _dbContext.GetTransactions()
                                               .Where(x => x.WalletId == walletId)
                                               .OrderByDescending(x => x.CreatedOn)
                                               .Select(x => new
                                               {
                                                   CreatedOn = x.CreatedOn,
                                                   Descripiton = x.Description,
                                                   Type = x.Type,
                                                   TypeName = x.Type.ToString(),
                                                   Kind = x.Kind,
                                                   KindName = x.Kind.ToString()
                                               })
                                               .ToListAsync(cancellationToken);

            return Results.Ok(transactions);
        });

        return endpoint;
    }

}
