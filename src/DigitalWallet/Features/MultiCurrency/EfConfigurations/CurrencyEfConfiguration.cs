namespace DigitalWallet.Features.MultiCurrency.EfConfigurations;

public class CurrencyEfConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable(WalletDbContextSchema.Currency.TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
               .IsRequired(true)
               .IsUnicode(false)
               .HasMaxLength(10);

        builder.HasIndex(x => x.Code)
               .IsUnique();

        builder.Property(x => x.Name)
               .IsRequired(true)
               .IsUnicode(true)
               .HasMaxLength(25);

        builder.Property(x => x.Ratio)
               .IsRequired(true)
               .HasColumnType(WalletDbContextSchema.DefaultAmountColumnType);

        builder.Property(x => x.ModifiedOnUtc)
               .IsRequired(true);
    }
}
