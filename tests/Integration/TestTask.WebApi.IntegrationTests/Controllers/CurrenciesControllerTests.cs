using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.IntegrationTests.Controllers;

public class CurrenciesControllerTests : IntegrationTest
{
    private const string Route = "api/currencies";
    public CurrenciesControllerTests(TestTaskWebApiApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task AddCurrency_Should_Add_Currency_To_Database_AND_Return_Created_Status_Code_when_valid_body_passed()
    {
        // arrange
        var (Title, AlphabeticCode, NumericCode) = Currencies.Supported[20];
        var body = new CurrencyAddModel(Title, AlphabeticCode, NumericCode);
        string token = await LoginAsAdminAsync();

        // act
        var response = await _httpClient
            .WithBearerToken(token)
            .PostAsJsonAsync(Route, body);

        // assert
        var currencyId = await response.DeserializeToAsync<CurrencyId>();
        var addedCurrency = await _dbContext.Currencies.SingleOrDefaultAsync(e => e.Id == currencyId);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        addedCurrency.Should().NotBeNull();
        addedCurrency?.AlphabeticCode.Should().Be(body.AlphabeticCode);
        addedCurrency?.NumericCode.Should().Be(body.NumericCode);
        addedCurrency?.Title.Should().Be(body.Title);
    }

    [Fact]
    public async Task AddCurrency_Should_Return_Forbidden_Status_Code_when_not_admin_try_to_add_currency()
    {
        // arrange
        const string noMatter = "noMatter";
        var body = new CurrencyAddModel(noMatter, noMatter, noMatter);
        string token = await LoginAsUserAsync();

        // act
        var response = await _httpClient
            .WithBearerToken(token)
            .PostAsJsonAsync(Route, body);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AddCurrency_Should_Return_BadRequest_Status_Code_when_invalid_body_passed()
    {
        // arrange
        var (invalidTitle, invalidAlphabeticCode, invalidNumericCode) = ("invalidTitle", "invalidAlphabeticCode", "invalidNumericCode");
        var body = new CurrencyAddModel(invalidTitle, invalidAlphabeticCode, invalidNumericCode);
        string token = await LoginAsAdminAsync();

        // act
        var response = await _httpClient
            .WithBearerToken(token)
            .PostAsJsonAsync(Route, body);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
