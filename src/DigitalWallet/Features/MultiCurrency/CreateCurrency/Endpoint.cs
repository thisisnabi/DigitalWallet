namespace DigitalWallet.Features.MultiCurrency.CreateCurrency;

public static class Endpoint
{
    public static IEndpointRouteBuilder AddCreateCurrencyEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/",
            async (CreateCurrencyRequest request, CurrencyService _service, CancellationToken cancellationToken) =>
            {
                var currencyId = await _service.CreateAsync(request.Code, request.Name, request.Ration, cancellationToken);
                return new CreateCurrencyResponse(currencyId.ToString());
            }).Validator<CreateCurrencyRequestValidator>();

        return endpoint;
    }

}
