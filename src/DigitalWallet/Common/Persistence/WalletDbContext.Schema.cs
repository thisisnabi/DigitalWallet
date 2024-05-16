 
namespace DigitalWallet.Common.Persistence;

public static class WalletDbContextSchema
{
    public const string DefaultSchema = "wallet";
    public const string DefaultConnectionStringName = "SvcDbContext";
    public const string DefaultAmountColumnType = "decimal(18,6)";

    public static class Currency
    {
        public const string TableName = "Currencies";
    }

    public static class Wallet
    {
        public const string TableName = "Wallets";
    }

    public static class Transactions
    {
        public const string TableName = "Transactions";
    }
}