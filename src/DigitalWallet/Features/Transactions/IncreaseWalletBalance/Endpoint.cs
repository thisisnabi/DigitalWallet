 using Carter;

 namespace DigitalWallet.Features.Transactions.IncreaseWalletBalance;

 public class Endpoint : ICarterModule
 {
     public void AddRoutes(IEndpointRouteBuilder app)
     {
         app
             .MapGroup(FeatureManager.Prefix)
             .WithTags(FeatureManager.EndpointTagName)
             .MapPost("/{wallet_id:guid:required}/increase", 
             async ([FromBody]IncreaseWalletBalanceRequest request, [FromRoute(Name = "wallet_id")]Guid Id, TransactionService _service ,CancellationToken cancellationToken) =>
             {
                 var walletId = WalletId.Create(Id);
                 await _service.IncreaseBalanceAsync(walletId, request.Amount, request.Description, cancellationToken);

                 return Results.Ok("Wallet balance increased successfully!");
             }).Validator<IncreaseWalletBalanceRequest>();
     }
 }