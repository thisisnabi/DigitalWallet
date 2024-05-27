using Carter;
using DigitalWallet.Common.Extensions;

namespace DigitalWallet.Features.UserWallet.ChangeTitle;

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup(FeatureManager.Prefix)
            .WithTags(FeatureManager.EndpointTagName)
            .MapPatch("/{wallet_id:guid:required}",
            async ([FromBody] ChangeTitleRequest request, [FromRoute(Name = "wallet_id")] Guid Id, WalletService _service, CancellationToken cancellationToken) =>
            {
                var walletId = WalletId.Create(Id);
                await _service.ChangeTitleAsync(walletId, request.Title, cancellationToken);

                return Results.Ok("Wallet title changed successfully!");
            }).Validator<ChangeTitleRequest>();

    }
}
