namespace DigitalWallet.Features.UserWallet.ChangeTitle;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddChangeTitleEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPatch("/{wallet_id:guid:required}",
            async ([FromBody] ChangeTitleRequest request, [FromRoute(Name = "wallet_id")] Guid Id, WalletService _service, CancellationToken cancellationToken) =>
            {
                var walletId = WalletId.Create(Id);
                await _service.ChangeTitleAsync(walletId, request.Title, cancellationToken);

                return Results.Ok("Wallet title changed successfully!");
            }).Validator<ChangeTitleRequest>();

        return endpoint;
    }

}
