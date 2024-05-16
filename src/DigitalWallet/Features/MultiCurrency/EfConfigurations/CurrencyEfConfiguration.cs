using DigitalWallet.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        builder.Property(x => x.RationToBase)
               .IsRequired(true)
               .HasColumnType("decimal(18,6)");

        builder.Property(x => x.LatestModifiedOnUtc)
               .HasDefaultValueSql("GETDATE()");
    }
}
