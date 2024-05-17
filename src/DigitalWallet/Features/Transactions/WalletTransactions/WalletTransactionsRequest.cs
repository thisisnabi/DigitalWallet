namespace DigitalWallet.Features.Transactions.WalletTransactions;

public record WalletTransactionsRequest(
    DateTime FromDate = default,
    DateTime ToDate = default);