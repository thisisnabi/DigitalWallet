namespace DigitalWallet.Features.UserWallet.ChangeTitle;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddChangeTitleEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPatch("/{wallet_id:guid:required}",
            async ([FromRoute(Name = "wallet_id")] Guid Id,[FromBody] ChangeTitleRequest request, WalletService _service, CancellationToken cancellationToken) =>
            {
                var walletId = WalletId.Create(Id);
                await _service.ChangeTitleAsync(walletId, request.Title, cancellationToken);

                return Results.Ok("Wallet title changed successfully!");
            }).Validator<ChangeTitleRequest>();

        return endpoint;
    }

}
