namespace DigitalWallet.Features.Transactions.WalletFunds;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddWalletFundsEndPoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/funds",
            async (WalletFundsRequest request, TransactionService _service, CancellationToken cancellationToken) =>
            {
                var swId = WalletId.Create(request.SourceWalletId);
                var dwId = WalletId.Create(request.DestinationWalletId);

                await _service.WalletFundsAsync(swId, dwId, request.Amount, request.Description, cancellationToken);

                return Results.Ok("Funds transferred successfully!");
            }).Validator<WalletFundsRequestValidator>();
        return endpoint;
    }

}
