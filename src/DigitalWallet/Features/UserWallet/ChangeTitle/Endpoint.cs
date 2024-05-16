using DigitalWallet.Common.Persistence;
using DigitalWallet.Features.UserWallet.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Features.UserWallet.ChangeTitle;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddChangeTitleWalletEndPoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPatch("/wallets/{id:guid}", async (
            [FromRoute(Name = "id")]Guid Id,
            ChangeTitleRequest request,
            CancellationToken ct,
            WalletService _service) =>
        {
            // TODO: Check data validation vy fluen validation
  
            await _service.ChangeTitleAsync(Id, request.Title, ct);

            // TODO: Use mapster for mapping
            return Results.Ok("Wallet title changed!");
        }).WithTags("Wallet");
        return endpoint;
    }

}
