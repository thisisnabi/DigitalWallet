namespace DigitalWallet.Features.UserWallet.Create;

public record CreateUserWalletRequest(
    string Title, 
    int CurrencyId,
    Guid UserId);
