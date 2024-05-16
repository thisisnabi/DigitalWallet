 
namespace DigitalWallet.Common.Persistence;

public static class WalletDbContextSchema
{
    public const string DefaultSchema = "wallet";
    public const string DefaultConnectionStringName = "SvcDbContext";


    public static class Currency
    {
        public const string TableName = "Currencies";
    }
}