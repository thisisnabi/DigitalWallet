using DigitalWallet.Common.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Features.Transactions.WalletTransactions;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddGetWalletTransactionsEndPoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/transactions/{wallet-id:guid}", async (
            [FromRoute(Name = "wallet-id")]Guid WalletId,
            CancellationToken ct,
            WalletDbContext _dbContext) =>
        {
             
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
