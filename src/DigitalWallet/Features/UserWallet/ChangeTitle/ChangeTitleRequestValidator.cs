namespace DigitalWallet.Features.UserWallet.ChangeTitle;

public class ChangeTitleRequestValidator : AbstractValidator<ChangeTitleRequest>
{
    public ChangeTitleRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull()
            .MaximumLength(30);
    }
}
