namespace DigitalWallet.Features.Transactions.DecreaseWalletBalance;

public record DecreaseWalletBalanceRequest(decimal Amount, string Description);
