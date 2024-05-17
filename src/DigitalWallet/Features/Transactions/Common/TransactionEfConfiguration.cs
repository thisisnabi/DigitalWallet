namespace DigitalWallet.Features.Transactions.Common;

public class TransactionEfConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(WalletDbContextSchema.Transactions.TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(id => id.Value,
                              value => TransactionId.Create(value));

        builder.Property(x => x.WalletId)
               .IsRequired(true)
               .ValueGeneratedNever()
               .HasConversion(id => id.Value,
                              value => WalletId.Create(value));

        builder.Property(x => x.Description)
               .IsRequired(true)
               .IsUnicode(true)
               .HasMaxLength(500);

        builder.Property(x => x.Amount)
               .IsRequired()
               .HasColumnType(WalletDbContextSchema.DefaultDecimalColumnType);

        builder.Property(x => x.CreatedOnUtc)
                .IsRequired(true);

        builder.Property(x => x.Kind)
               .IsRequired(true);

        builder.Property(x => x.Type)
               .IsRequired(true);
    }
}
