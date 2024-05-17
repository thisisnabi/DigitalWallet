namespace DigitalWallet.Features.UserWallet.Common;

public record WalletId(Guid Value)
{
    public static WalletId CreateUniqueId() => new WalletId(Guid.NewGuid());

    public static WalletId Create(Guid value) => new WalletId(value);

    public override string ToString()
    {
        return Value.ToString();
    }
};