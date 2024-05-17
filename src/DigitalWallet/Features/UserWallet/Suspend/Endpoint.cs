namespace DigitalWallet.Features.UserWallet.Suspend;

public static class Endpoint
{
    public static IEndpointRouteBuilder AddSuspendWalletEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPatch("/{wallet-id:guid:required}/suspend",
            async ([FromRoute(Name = "wallet-id")] Guid Id, WalletService _service, CancellationToken cancellationToken) =>
            {
                var walletId = WalletId.Create(Id);
                await _service.SuspendAsync(walletId, cancellationToken);

                return Results.Ok("Wallet suspended successfully!");
            });

        return endpoint;
    }
}
