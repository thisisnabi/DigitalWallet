using Carter;

namespace DigitalWallet.Features.UserWallet.GetBalance;

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGroup(FeatureManager.Prefix)
            .WithTags(FeatureManager.EndpointTagName)
            .MapGet("/{wallet_id:guid:required}/balance/",
                    async ([FromRoute(Name = "wallet_id")] Guid Id, WalletDbContextReadOnly _dbContext, CancellationToken cancellationToken) =>
                {
                    var walletId = WalletId.Create(Id);
                    var wallet = await _dbContext.GetWallets()
                                                 .FirstOrDefaultAsync(x => x.Id == walletId, cancellationToken);
        
                    if (wallet is null)
                    {
                        throw new WalletNotFoundException(walletId);
                    }
        
                    return Results.Ok(new
                    {
                        WalletId = walletId.ToString(),
                        Balance = wallet.Balance
                    });
                });
    }
}