namespace DigitalWallet.Features.UserWallet;

public record UserId(Guid Value)
{
    public static UserId CreateUniqueId() => new UserId(Guid.NewGuid());

    public static UserId Create(Guid value) => new UserId(value);

    public override string ToString()
    {
        return Value.ToString();
    }
};