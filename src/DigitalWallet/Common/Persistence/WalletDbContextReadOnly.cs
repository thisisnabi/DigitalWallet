namespace DigitalWallet.Common.Persistence;

public class WalletDbContextReadOnly : DbContext
{
    public WalletDbContextReadOnly(DbContextOptions<WalletDbContext> dbContextOptions) : base(dbContextOptions)
    {

    }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(WalletDbContextSchema.DefaultSchema);

        var assembly = typeof(IAssemblyMarker).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }

    public IQueryable<Transaction> GetTransactions() => 
           Set<Transaction>().AsQueryable();
}
