namespace DigitalWallet.Features.UserWallet.DTOs;

public record CreateUserWallet(string Title, Guid UserId, int CurrencyId);