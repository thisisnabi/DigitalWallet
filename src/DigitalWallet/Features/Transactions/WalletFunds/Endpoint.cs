using Carter;
using DigitalWallet.Common.Extensions;

namespace DigitalWallet.Features.Transactions.WalletFunds;

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup(FeatureManager.Prefix)
            .WithTags(FeatureManager.EndpointTagName)
            .MapPost("/funds",
            async ([FromBody]WalletFundsRequest request, TransactionService service, CancellationToken cancellationToken) =>
            {
                var swId = WalletId.Create(request.SourceWalletId);
                var dwId = WalletId.Create(request.DestinationWalletId);

                await service.WalletFundsAsync(swId, dwId, request.Amount, request.Description, cancellationToken);

                return Results.Ok("Funds transferred successfully!");
            }).Validator<WalletFundsRequest>();
    }
}