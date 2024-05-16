namespace DigitalWallet.Features.Transactions.DecrementWalletBalance;

public record DecrementWalletBalanceRequest(
    Guid WalletId,
    decimal Amount, 
    string Description);
