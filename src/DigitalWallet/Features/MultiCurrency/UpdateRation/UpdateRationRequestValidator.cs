namespace DigitalWallet.Features.MultiCurrency.UpdateRation;

public class UpdateRationRequestValidator : AbstractValidator<UpdateRationRequest>
{
    public UpdateRationRequestValidator()
    {
        RuleFor(x => x.Ratio)
            .GreaterThan(0);
    }
}
