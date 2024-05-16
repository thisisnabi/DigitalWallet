using DigitalWallet.Common.Persistence;
using DigitalWallet.Features.MultiCurrency.DTOs;

namespace DigitalWallet.Features.MultiCurrency.Create;

public static class Endpoint
{

    public static IEndpointRouteBuilder AddCreateCurrencyEndPoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/Currency", async (CreateCurrencyRequest request,
            WalletDbContext dbContext, CancellationToken ct, CurrencyService _service) =>
        {
            // TODO: Check data validation vy fluen validation
            // TODO: Use mapster for mapping

            var dto = new CreateCurrency(request.Code, request.Name, request.IsBased, request.Ration);
            var response = await _service.CreateAsync(dto, ct);
             
            // TODO: Use mapster for mapping
            return new CreateCurrencyResponse(response.Id, response.Name, response.Code);
        }).WithTags("Currency");
        return endpoint;
    }

}
