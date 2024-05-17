 namespace DigitalWallet.Features.Transactions.IncreaseWalletBalance;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddIncreaseWalletBalanceEndPoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/transactions/{wallet-id:guid:required}/increase", 
            async ([FromRoute(Name = "wallet-id")]Guid Id,IncreaseWalletBalanceRequest request, TransactionService _service ,CancellationToken cancellationToken) =>
            {
                var walletId = WalletId.Create(Id);
                await _service.IncreaseBalanceAsync(walletId, request.Amount, request.Description, cancellationToken);

                return Results.Ok("Wallet balance increased successfully!");
        }).Validator<IncreaseWalletBalanceRequestValidator>();
        return endpoint;
    }

}
