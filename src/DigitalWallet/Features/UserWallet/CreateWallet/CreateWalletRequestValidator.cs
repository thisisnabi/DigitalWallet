namespace DigitalWallet.Features.UserWallet.CreateWallet;

public class CreateWalletRequestValidator : AbstractValidator<CreateWalletRequest>
{
    public CreateWalletRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull();

        RuleFor(x => x.CurrencyId)
             .NotNull();

        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull()
            .MaximumLength(30);
    }
}
