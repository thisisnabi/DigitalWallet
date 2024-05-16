namespace DigitalWallet.Features.MultiCurrency.DTOs;

public record CreateCurrency(string Code,
                                    string Name,
                                    bool IsBased,
                                    decimal RationToBase);