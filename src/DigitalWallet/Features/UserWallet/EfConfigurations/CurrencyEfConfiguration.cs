using DigitalWallet.Common.Persistence;
using DigitalWallet.Features.UserWallet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Features.UserWallet.EfConfiguration;

public class WalletEfConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable(WalletDbContextSchema.Wallet.TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
               .IsRequired();

        builder.Property(x => x.Title)
               .IsRequired(false)
               .IsUnicode(true)
               .HasMaxLength(30);

        builder.Property(x => x.Balance)
               .IsRequired()
               .HasColumnType("decimal(18,6)");

        builder.Property(x => x.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.CurrencyId)
               .IsRequired(true);

        builder.Property(x => x.Status)
               .IsRequired(true)
               .HasDefaultValue(WalletStatus.None);
    }
}
