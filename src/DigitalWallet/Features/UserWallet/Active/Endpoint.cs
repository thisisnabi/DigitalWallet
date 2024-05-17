namespace DigitalWallet.Features.UserWallet.Active;

public static class Endpoint
{
    public static IEndpointRouteBuilder AddActiveWalletEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPatch("/{wallet_id:guid:required}/active",
            async ([FromRoute(Name = "wallet_id")] Guid Id, WalletService _service, CancellationToken cancellationToken) =>
            {
                var walletId = WalletId.Create(Id);
                await _service.ActiveAsync(walletId, cancellationToken);

                return Results.Ok("Wallet activated successfully!");
            });

        return endpoint;
    }
}
