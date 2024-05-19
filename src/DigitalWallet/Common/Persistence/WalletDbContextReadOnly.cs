namespace DigitalWallet.Common.Persistence;

public class WalletDbContextReadOnly : DbContext
{
    public WalletDbContextReadOnly(DbContextOptions<WalletDbContextReadOnly> dbContextOptions) : base(dbContextOptions)
    {

    }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(WalletDbContextSchema.DefaultSchema);

        var assembly = typeof(IAssemblyMarker).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }

    public IQueryable<Transaction> GetTransactions() => Set<Transaction>().AsQueryable();

    public IQueryable<Wallet> GetWallets() => Set<Wallet>().AsQueryable();

    public IQueryable<Currency> GetCurrencies() => Set<Currency>().AsQueryable();

    public override int SaveChanges()
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }
}
