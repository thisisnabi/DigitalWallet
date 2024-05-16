namespace DigitalWallet.Features.MultiCurrency.UpdateRatio;

public class UpdateRatioRequestValidator : AbstractValidator<UpdateRatioRequest>
{
    public UpdateRatioRequestValidator()
    {
        RuleFor(x => x.Ratio)
            .GreaterThan(0);
    }
}
