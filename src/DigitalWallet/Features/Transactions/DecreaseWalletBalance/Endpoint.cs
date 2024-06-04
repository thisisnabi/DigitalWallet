using Carter;
using DigitalWallet.Common.Extensions;

namespace DigitalWallet.Features.Transactions.DecreaseWalletBalance;


public class Endpoint : ICarterModule
 {
     public void AddRoutes(IEndpointRouteBuilder app)
     {
         app
             .MapGroup(FeatureManager.Prefix)
             .WithTags(FeatureManager.EndpointTagName)
             .MapPost("/{wallet_id:guid:required}/decrease", 
             async ([FromBody]DecreaseWalletBalanceRequest request, [FromRoute(Name = "wallet_id")]Guid Id, TransactionService _service ,CancellationToken cancellationToken) =>
             {
                 var walletId = WalletId.Create(Id);
                 await _service.DecreaseBalanceAsync(walletId, request.Amount, request.Description, cancellationToken);

                 return Results.Ok("Wallet balance decreased successfully!");
             }).Validator<DecreaseWalletBalanceRequest>();
     }
 }
