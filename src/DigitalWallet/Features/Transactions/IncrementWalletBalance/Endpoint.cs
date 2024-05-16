using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Features.Transactions.IncrementWalletBalance;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddIncrementWalletBalanceEndPoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/transactions/increase", async (
            IncreamentWalletBalanceRequest request,
            CancellationToken ct,
            TransactionService _service) =>
        {
            // TODO: Check data validation vy fluen validation

            await _service.IncrementBalanceAsync(request.WalletId, request.Amount, request.Description, ct);

            // TODO: Use mapster for mapping
            return Results.Ok("wallet balance increased!");
        }).WithTags("Transaction");
        return endpoint;
    }

}
