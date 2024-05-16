namespace DigitalWallet.Features.MultiCurrency.UpdateRatio;

public static class Endpoint
{
    public static IEndpointRouteBuilder AddUpdateRatioEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPatch("/{currency-id:guid:required}",
            async ([FromRoute(Name = "currency-id")] string Id, UpdateRatioRequest request, CurrencyService _service, CancellationToken cancellationToken) =>
            {
                var currencyId = CurrencyId.Create(Guid.Parse(Id));
                await _service.UpdateRationAsync(currencyId, request.Ratio, cancellationToken);

                return Results.Ok("Currency ratio updated successfully!");
            }).Validator<UpdateRatioRequestValidator>()
          .WithTags(FeatureManager.EndpointTagName);

        return endpoint;
    }
}
