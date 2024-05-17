namespace DigitalWallet.Features.UserWallet.Suspend;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddSuspendWalletEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPatch("/{wallet-id:guid:required}/suspend",
            async ([FromRoute(Name = "wallet-id")] Guid Id,
            CancellationToken ct,
            WalletService _service) =>
        {
            var walletId = WalletId.Create(Id);
            await _service.SuspendAsync(walletId, ct);

            return Results.Ok("Wallet suspended successfully!");
        }).WithTags("Wallet");
        return endpoint;
    }

}
