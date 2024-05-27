using Carter;
using DigitalWallet.Common.Extensions;

namespace DigitalWallet.Features.MultiCurrency.CreateCurrency;

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup(FeatureManager.Prefix)
            .WithTags(FeatureManager.EndpointTagName)
            .MapPost("/",
            async ([FromBody]CreateCurrencyRequest request, CurrencyService service, CancellationToken cancellationToken) =>
            {
                var currencyId = await service.CreateAsync(request.Code, request.Name, request.Ratio, cancellationToken);
                return new CreateCurrencyResponse(currencyId.ToString());
            }).Validator<CreateCurrencyRequest>();
    }
}
