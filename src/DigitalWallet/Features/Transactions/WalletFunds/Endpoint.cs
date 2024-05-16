
namespace DigitalWallet.Features.Transactions.WalletFunds;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddWalletFundsEndPoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/transactions/funds", async (
            WalletFundsRequest request,
            CancellationToken ct,
            TransactionService _service) =>
        {
            // TODO: Check data validation vy fluen validation

            await _service.WalletFundsAsync(
                request.SourceWalletId,
                request.DestinationWalletId,
                request.Amount,
                request.Description,
                ct);

            // TODO: Use mapster for mapping
            return Results.Ok("done!");
        }).WithTags("Transaction");
        return endpoint;
    }

}
