using DigitalWallet.Features.UserWallet.Common;

namespace DigitalWallet.Features.UserWallet.Suspend;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddSuspendWalletEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPatch("/wallets/{id:guid}/suspend", async (
            [FromRoute(Name = "id")] Guid Id,
            CancellationToken ct,
            WalletService _service) =>
        {
            // TODO: Check data validation vy fluen validation

            await _service.SuspendAsync(Id, ct);

            // TODO: Use mapster for mapping
            return Results.Ok("Wallet suspended!");
        }).WithTags("Wallet");
        return endpoint;
    }

}
