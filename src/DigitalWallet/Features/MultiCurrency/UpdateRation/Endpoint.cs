using DigitalWallet.Common.Persistence;
using DigitalWallet.Features.MultiCurrency.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Features.MultiCurrency.UpdateRation;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddUpdateRationCurrencyEndPoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPatch("/Currency/{id:required}", async ([FromRoute(Name = "id")]int Id,
                                                            UpdateRationRequest request,
            WalletDbContext dbContext, CancellationToken ct, CurrencyService _service) =>
        {
            // TODO: Check data validation vy fluen validation
            // TODO: Use mapster for mapping

            var dto = new UpdateRationCurrency(Id, request.Ration);
            await _service.UpdateRationAsync(dto, ct);
              
            return Results.Ok("currancy ration updated!");
        }).WithTags("Currency");
        return endpoint;
    }

}
