namespace DigitalWallet.Features.Transactions.WalletTransactions;

public record WalletTransactionsRequest(
    [FromQuery(Name = "from_date")] DateTime FromDate,
    [FromQuery(Name = "to_date")] DateTime ToDate);