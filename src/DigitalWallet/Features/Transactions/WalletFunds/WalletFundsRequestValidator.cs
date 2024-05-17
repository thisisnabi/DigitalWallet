namespace DigitalWallet.Features.Transactions.WalletFunds;

public class WalletFundsRequestValidator : AbstractValidator<WalletFundsRequest>
{
    public WalletFundsRequestValidator()
    {
        RuleFor(x => x.Description)
             .NotEmpty()
             .NotNull()
             .MaximumLength(500);

        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.SourceWalletId)
            .NotNull();

        RuleFor(x => x.DestinationWalletId)
            .NotNull();
    }
}
