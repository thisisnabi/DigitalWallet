namespace DigitalWallet.Common.Persistence;

public class WalletDbContext : DbContext
{
    public WalletDbContext(DbContextOptions<WalletDbContext> dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<Wallet> Wallets => Set<Wallet>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(WalletDbContextSchema.DefaultSchema);

        var assembly = typeof(IAssemblyMarker).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}
