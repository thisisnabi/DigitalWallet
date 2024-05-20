 using Carter;

 namespace DigitalWallet.Features.UserWallet.GetTransactions;

 public class Endpoint : ICarterModule
 {
     public void AddRoutes(IEndpointRouteBuilder app)
     {
         app
             .MapGroup(FeatureManager.Prefix)
             .WithTags(FeatureManager.EndpointTagName)
             .MapGet("/{wallet_id:guid:required}/transactions/",
             async ([FromRoute(Name = "wallet_id")]Guid Id, WalletDbContextReadOnly _dbContext, CancellationToken cancellationToken) =>
             {

                 var walletId = WalletId.Create(Id);

                 var transactions = await _dbContext.GetTransactions()
                     .Where(x => x.WalletId == walletId)
                     .OrderByDescending(x => x.CreatedOnUtc)
                     .Select(x => new
                     {
                         CreatedOn = x.CreatedOnUtc,
                         Descripiton = x.Description,
                         Type = x.Type,
                         TypeName = x.Type.ToString(),
                         Kind = x.Kind,
                         KindName = x.Kind.ToString()
                     })
                     .ToListAsync(cancellationToken);

                 return Results.Ok(transactions);
             });

     }
 }