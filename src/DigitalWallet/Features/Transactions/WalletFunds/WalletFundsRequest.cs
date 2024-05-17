namespace DigitalWallet.Features.Transactions.WalletFunds;

public record WalletFundsRequest(Guid SourceWalletId, Guid DestinationWalletId, decimal Amount,  string Description);
