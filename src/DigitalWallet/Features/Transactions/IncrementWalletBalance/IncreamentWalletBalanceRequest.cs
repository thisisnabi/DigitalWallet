namespace DigitalWallet.Features.Transactions.IncrementWalletBalance;

public record IncreamentWalletBalanceRequest(
    Guid WalletId,
    decimal Amount, 
    string Description);
