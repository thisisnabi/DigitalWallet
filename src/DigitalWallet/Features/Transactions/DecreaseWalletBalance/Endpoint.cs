﻿ namespace DigitalWallet.Features.Transactions.DecreaseWalletBalance;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddDecreaseWalletBalanceEndPoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/{wallet_id:guid:required}/decrease", 
            async ([FromRoute(Name = "wallet_id")]Guid Id, [FromBody]DecreaseWalletBalanceRequest request, TransactionService _service ,CancellationToken cancellationToken) =>
            {
                var walletId = WalletId.Create(Id);
                await _service.DecreaseBalanceAsync(walletId, request.Amount, request.Description, cancellationToken);

                return Results.Ok("Wallet balance decreased successfully!");
        }).Validator<DecreaseWalletBalanceRequest>();
        return endpoint;
    }

}
