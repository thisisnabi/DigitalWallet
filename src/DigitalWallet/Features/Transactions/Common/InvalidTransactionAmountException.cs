namespace DigitalWallet.Features.Transactions.Common;
 
public class InvalidTransactionAmountException : Exception
{
    private const string _message = "You can't make a zero transaction.";

    public InvalidTransactionAmountException() : base(_message)
    {
    }

    public static void Throw(decimal amount)
    {
        if (amount == 0)
        { 
            throw new InvalidTransactionAmountException();
        }
    }
}