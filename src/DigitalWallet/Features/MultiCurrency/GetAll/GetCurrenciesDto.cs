namespace DigitalWallet.Features.MultiCurrency.GetAll;

public class GetCurrenciesDto
{
    public string Id{get;set;} 
    public string Name{get;set;}
    public string Code{get;set;}
    public decimal Ratio{get;set;}
}