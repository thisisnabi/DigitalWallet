namespace DigitalWallet.Features.Transactions.IncreaseWalletBalance;

public class IncreaseWalletBalanceRequestValidator : AbstractValidator<IncreaseWalletBalanceRequest>
{
    public IncreaseWalletBalanceRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull()
            .MaximumLength(500);

        RuleFor(x => x.Amount)
            .GreaterThan(0);
    }
}
