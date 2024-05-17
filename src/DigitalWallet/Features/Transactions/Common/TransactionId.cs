namespace DigitalWallet.Features.Transactions.Common;
 
public record TransactionId(Guid Value)
{
    public static TransactionId CreateUniqueId() => new TransactionId(Guid.NewGuid());

    public static TransactionId Create(Guid value) => new TransactionId(value);

    public override string ToString()
    {
        return Value.ToString();
    }
};