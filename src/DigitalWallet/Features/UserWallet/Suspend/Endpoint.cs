using Carter;

namespace DigitalWallet.Features.UserWallet.Suspend;

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup(FeatureManager.Prefix)
            .WithTags(FeatureManager.EndpointTagName)
            .MapPatch("/{wallet_id:guid:required}/suspend",
            async ([FromRoute(Name = "wallet_id")] Guid Id, WalletService _service, CancellationToken cancellationToken) =>
            {
                var walletId = WalletId.Create(Id);
                await _service.SuspendAsync(walletId, cancellationToken);

                return Results.Ok("Wallet suspended successfully!");
            });

    }
}
