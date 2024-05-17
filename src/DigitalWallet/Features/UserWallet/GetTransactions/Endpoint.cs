 namespace DigitalWallet.Features.UserWallet.GetTransactions;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddGetTransactionsEndPoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/{wallet-id:guid:required}/transactions/",
            async ([FromRoute(Name = "wallet-id")]Guid Id, WalletDbContext _dbContext, CancellationToken cancellationToken) =>
        {

            var walletId = WalletId.Create(Id);

            return Results.Ok(_dbContext.Transactions.Where(x => x.WalletId == WalletId)
                                                     .OrderByDescending(x => x.CreatedOn)
                                                     .Select(x => new { 
                                                        CreatedOn = x.CreatedOn,
                                                        Descripiton = x.Description,
                                                        Type = x.Type,
                                                        TypeName = x.Type.ToString(),
                                                        Kind = x.Kind,
                                                        KindName = x.Kind.ToString()
                                                     }));
        }).WithTags("Transaction");
        return endpoint;
    }

}
