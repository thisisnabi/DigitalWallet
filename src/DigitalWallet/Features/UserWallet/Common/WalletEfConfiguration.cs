namespace DigitalWallet.Features.UserWallet.Common;

public class WalletEfConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable(WalletDbContextSchema.Wallet.TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(id => id.Value,
                              value => WalletId.Create(value));

        builder.Property(x => x.UserId)
               .IsRequired(true)
               .ValueGeneratedNever()
               .HasConversion(id => id.Value,
                              value => UserId.Create(value));

        builder.Property(x => x.CurrencyId)
               .IsRequired(true)
               .ValueGeneratedNever()
               .HasConversion(id => id.Value,
                              value => CurrencyId.Create(value));

        builder.Property(x => x.Title)
               .IsRequired(false)
               .IsUnicode(true)
               .HasMaxLength(30);

        builder.Property(x => x.Balance)
               .IsRequired()
               .HasColumnType(WalletDbContextSchema.DefaultDecimalColumnType);

        builder.Property(x => x.CreatedOn)
               .IsRequired(true);

        builder.Property(x => x.Status)
               .IsRequired(true);

        builder.HasMany(x => x.Transactions)
                .WithOne(x => x.Wallet)
                .HasForeignKey(x => x.WalletId);
    }
}
