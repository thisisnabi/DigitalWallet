

using DigitalWallet.Features.MultiCurrency;
using DigitalWallet.Features.MultiCurrency.CreateCurrency;
using DigitalWallet.IntegrationTests.Factory;
using FluentAssertions;
using System.Net.Http.Json;

namespace DigitalWallet.IntegrationTests.CurrencyTest;

public class CreateCurrencyEndpointTests : IClassFixture<DigitalWalletApiFactory>
{
    private readonly HttpClient _httpClient;
    public readonly DigitalWalletApiFactory _digitalWalletApiFactory;

    public CreateCurrencyEndpointTests(DigitalWalletApiFactory digitalWalletApiFactory)
    {
        _digitalWalletApiFactory = digitalWalletApiFactory;
        _httpClient = _digitalWalletApiFactory.CreateClient();
    }

    [Fact(DisplayName = "Create Currency With Invalid Raito")]
    public async Task Create_Currency_With_Invalid_Ratio()
    {
        // Arrange
        CreateCurrencyRequest request = new("rial", "rial", 0);

        // Act
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"{FeatureManager.Prefix}/", request);

        // Assert
        httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.UnprocessableEntity);
    }
}
