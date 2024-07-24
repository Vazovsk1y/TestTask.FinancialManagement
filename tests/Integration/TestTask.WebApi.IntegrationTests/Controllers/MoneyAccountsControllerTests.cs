using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Net;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;
using TestTask.WebApi.ViewModels;
using FluentAssertions;

namespace TestTask.WebApi.IntegrationTests.Controllers;

public class MoneyAccountsControllerTests(TestTaskWebApiApplicationFactory factory) : IntegrationTest(factory)
{
    private const string Route = "api/money-accounts";

    [Fact]
    public async Task CreateMoneyAccount_Should_Add_MoneyAccount_To_Database_AND_Return_Created_Status_Code_when_valid_body_passed()
    {
        // arrange
        var (title, alphabeticCode, numericCode) = Currencies.Supported[8];
        var accountCurrency = new Currency
        {
            AlphabeticCode = alphabeticCode,
            NumericCode = numericCode,
            Title = title,
        };
        _dbContext.Currencies.Add(accountCurrency);
        await _dbContext.SaveChangesAsync();

        var body = new MoneyAccountCreateModel(accountCurrency.Id.Value);
        string token = await LoginAsUserAsync();

        // act
        var response = await _httpClient
            .WithBearerToken(token)
            .PostAsJsonAsync(Route, body);

        // assert
        var moneyAccountId = await response.DeserializeToAsync<MoneyAccountId>();
        var moneyAccount = await _dbContext.MoneyAccounts.SingleOrDefaultAsync(e => e.Id == moneyAccountId);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        moneyAccount.Should().NotBeNull();
        moneyAccount?.CurrencyId.Should().BeEquivalentTo(new CurrencyId(body.CurrencyId));
        moneyAccount?.UserId.Should().BeEquivalentTo(User.Id);
    }

    [Fact]
    public async Task CreateMoneyAccount_Should_Return_BadRequest_Status_Code_when_invalid_body_passed()
    {
        // arrange
        Guid randomCurrencyIdThatNotExistsInDatabase = Guid.NewGuid();
        var body = new MoneyAccountCreateModel(randomCurrencyIdThatNotExistsInDatabase);
        string token = await LoginAsUserAsync();

        // act
        var response = await _httpClient
            .WithBearerToken(token)
            .PostAsJsonAsync(Route, body);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateMoneyAccount_Should_Return_Unauthorized_Status_Code_when_unauthenticated_user_try_to_add_moneyAccount()
    {
        // arrange
        Guid randomCurrencyIdThatNotExistsInDatabase = Guid.NewGuid();
        var body = new MoneyAccountCreateModel(randomCurrencyIdThatNotExistsInDatabase);

        // act
        var response = await _httpClient
            .PostAsJsonAsync(Route, body);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
