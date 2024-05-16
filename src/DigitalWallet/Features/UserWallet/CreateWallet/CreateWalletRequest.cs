namespace DigitalWallet.Features.UserWallet.CreateWallet;

public record CreateWalletRequest(string Title, Guid CurrencyId, Guid UserId);
