namespace DigitalWallet.Features.UserWallet.GetBalance;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddGetBalanceEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/{wallet_id:guid:required}/balance/",
            async ([FromRoute(Name = "wallet_id")] Guid Id, WalletDbContextReadOnly _dbContext, CancellationToken cancellationToken) =>
        {

            var walletId = WalletId.Create(Id);
            var wallet = await _dbContext.GetWallets()
                                         .FirstOrDefaultAsync(x => x.Id == walletId, cancellationToken);

            if (wallet is null) throw new WalletNotFoundException(walletId);
              
            return Results.Ok(new
            {
                WalletId = walletId,
                Balance = wallet.Balance
            });
        });

        return endpoint;
    }

}
