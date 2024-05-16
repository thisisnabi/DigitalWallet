namespace DigitalWallet.Features.MultiCurrency.Common;

public class CurrencyEfConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable(WalletDbContextSchema.Currency.TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(id => id.Value,
                              value => CurrencyId.Create(value));


        builder.Property(x => x.Code)
               .IsRequired(true)
               .IsUnicode(false)
               .HasMaxLength(10);

        builder.HasIndex(x => x.Code)
               .IsUnique(true);

        builder.Property(x => x.Name)
               .IsRequired(true)
               .IsUnicode(true)
               .HasMaxLength(30);

        builder.Property(x => x.Ratio)
               .IsRequired(true)
               .HasColumnType(WalletDbContextSchema.DefaultDecimalColumnType);

        builder.Property(x => x.ModifiedOnUtc)
               .IsRequired(true);
    }
}
