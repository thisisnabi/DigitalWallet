using DigitalWallet.Common.Persistence;
using DigitalWallet.Features.UserWallet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Features.Transactions.EfConfiguration;

public class TransactionEfConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(WalletDbContextSchema.Transactions.TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.WalletId)
               .IsRequired();

        builder.Property(x => x.Description)
               .IsRequired(false)
               .IsUnicode(true)
               .HasMaxLength(500);

        builder.Property(x => x.Amount)
               .IsRequired()
               .HasColumnType("decimal(18,6)");

        builder.Property(x => x.CreatedOn)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.Kind)
               .IsRequired(true);

        builder.Property(x => x.Type)
               .IsRequired(true);
    }
}
