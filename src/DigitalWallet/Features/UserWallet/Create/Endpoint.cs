using DigitalWallet.Common.Persistence;
using DigitalWallet.Features.UserWallet.DTOs;

namespace DigitalWallet.Features.UserWallet.Create;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddCreateUserWalletEndPoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/wallets", async (CreateUserWalletRequest request,
            CancellationToken ct,
            WalletService _service) =>
        {
            // TODO: Check data validation vy fluen validation
            // TODO: Use mapster for mapping

            var dto = new CreateUserWallet(request.Title, request.UserId, request.CurrencyId);
           
            var walletId = await _service.CreateAsync(dto, ct);

            // TODO: Use mapster for mapping
            return new CreateUserWalletResponse(walletId);
        }).WithTags("Wallet");
        return endpoint;
    }

}
