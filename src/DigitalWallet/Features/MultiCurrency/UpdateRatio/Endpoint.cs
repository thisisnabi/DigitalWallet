namespace DigitalWallet.Features.MultiCurrency.UpdateRatio;

public static class Endpoint
{
    public static IEndpointRouteBuilder AddUpdateRatioEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPatch("/{currency_id:guid:required}",
            async ([FromBody] UpdateRatioRequest request, [FromRoute(Name = "currency_id")] Guid Id, CurrencyService _service, CancellationToken cancellationToken) =>
            {
                var currencyId = CurrencyId.Create(Id);
                await _service.UpdateRationAsync(currencyId, request.Ratio, cancellationToken);

                return Results.Ok("Currency ratio updated successfully!");
            }).Validator<UpdateRatioRequest>();

        return endpoint;
    }
}
