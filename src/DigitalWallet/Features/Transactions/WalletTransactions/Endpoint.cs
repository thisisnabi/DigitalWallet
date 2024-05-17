using Azure.Core;
using DigitalWallet.Common.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace DigitalWallet.Features.Transactions.WalletTransactions;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddGetOnRangeTransactionsEndPoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/{wallet_id:guid:required}",
            async ([FromRoute(Name = "wallet_id")]Guid Id, [AsParameters]WalletTransactionsRequest requst,  WalletDbContextReadOnly _dbContext, CancellationToken cancellationToken) =>
            {
                var walletId = WalletId.Create(Id);

                var transactions = await _dbContext.GetTransactions()
                                                   .Where(x => x.WalletId == walletId)
                                                   .Where(x => x.CreatedOn >= requst.FromDate && x.CreatedOn <= requst.ToDate)
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
