namespace DigitalWallet.Features.UserWallet.Common;
 
public class InsufficientBalanceException : Exception
{
    private const string _message = "Insufficient balance in the wallet to perform the operation.";

    public InsufficientBalanceException() : base(_message)
    {
    }

    [DoesNotReturn]
    public static void Throw()
    {
        throw new InsufficientBalanceException();
    }
}