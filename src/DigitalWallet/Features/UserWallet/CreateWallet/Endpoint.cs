using Carter;

namespace DigitalWallet.Features.UserWallet.CreateWallet;

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup(FeatureManager.Prefix)
            .WithTags(FeatureManager.EndpointTagName)
            .MapPost("/",
            async ([FromBody] CreateWalletRequest request, WalletService _service, CancellationToken cancellationToken) =>
            {
                var currencyId = CurrencyId.Create(request.CurrencyId);
                var userId = UserId.Create(request.UserId);

                var walletId = await _service.CreateAsync(userId, currencyId, request.Title, cancellationToken);

                return new CreateWalletResponse(walletId.ToString());
            }).Validator<CreateWalletRequest>();

    }
}