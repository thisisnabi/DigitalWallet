namespace DigitalWallet.Features.MultiCurrency.Create;

public record CreateCurrencyRequest(string Code, string Name, decimal Ration);
