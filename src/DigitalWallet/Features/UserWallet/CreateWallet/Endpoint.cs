namespace DigitalWallet.Features.UserWallet.CreateWallet;

public static class Endpoint
{
    public static IEndpointRouteBuilder AddCreateWalletEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/",
            async (CreateWalletRequest request, WalletService _service, CancellationToken cancellationToken) =>
            {
                var currencyId = CurrencyId.Create(request.CurrencyId);
                var userId = UserId.Create(request.UserId);

                var walletId = await _service.CreateAsync(userId, currencyId, request.Title, cancellationToken);

                return new CreateWalletResponse(walletId.ToString());
            }).Validator<CreateWalletRequestValidator>();

        return endpoint;
    }
}
