using Carter;
using DigitalWallet.Common.Extensions;

namespace DigitalWallet.Features.MultiCurrency.UpdateRatio;

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup(FeatureManager.Prefix)
            .WithTags(FeatureManager.EndpointTagName)
            .MapPatch("/{currency_id:guid:required}",
            async ([FromBody] UpdateRatioRequest request, [FromRoute(Name = "currency_id")] Guid id,
                CurrencyService service, CancellationToken cancellationToken) =>
            {
                var currencyId = CurrencyId.Create(id);
                await service.UpdateRationAsync(currencyId, request.Ratio, cancellationToken);

                return Results.Ok("Currency ratio updated successfully!");
            }).Validator<UpdateRatioRequest>();
    }
}