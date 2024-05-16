 
namespace DigitalWallet.Features.Transactions.DecrementWalletBalance;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddDecrementWalletBalanceEndPoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/transactions/decrease", async (
            DecrementWalletBalanceRequest request,
            CancellationToken ct,
            TransactionService _service) =>
        {
            // TODO: Check data validation vy fluen validation

            await _service.DecrementBalanceAsync(request.WalletId, request.Amount, request.Description, ct);

            // TODO: Use mapster for mapping
            return Results.Ok("wallet balance decreased!");
        }).WithTags("Transaction");
        return endpoint;
    }

}
