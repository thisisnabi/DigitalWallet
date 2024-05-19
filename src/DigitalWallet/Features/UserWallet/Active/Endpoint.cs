using Carter;

namespace DigitalWallet.Features.UserWallet.Active;


public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup(FeatureManager.Prefix)
            .WithTags(FeatureManager.EndpointTagName)
            .MapPatch("/{wallet_id:guid:required}/active",
            async ([FromRoute(Name = "wallet_id")] Guid Id, WalletService service, CancellationToken cancellationToken) =>
            {
                var walletId = WalletId.Create(Id);
                await service.ActiveAsync(walletId, cancellationToken);

                return Results.Ok("Wallet activated successfully!");
            });

    }
}