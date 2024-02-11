using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.IntegrationTests.Controllers;

public class CommissionsControllerTests : IntegrationTest
{
    private const string Route = "api/commissions";
    public CommissionsControllerTests(TestTaskWebApiApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task AddCommission_Should_Add_Commission_To_Database_AND_Return_Created_Status_Code_when_valid_body_passed()
    {
        // arrange
        var currencyFrom = new Currency
        {
            AlphabeticCode = Currencies.Supported[1].AlphabeticCode,
            NumericCode = Currencies.Supported[1].NumericCode,
            Title = Currencies.Supported[1].Title,
        };
        var currencyTo = new Currency
        {
            AlphabeticCode = Currencies.Supported[2].AlphabeticCode,
            NumericCode = Currencies.Supported[2].NumericCode,
            Title = Currencies.Supported[2].Title,
        };

        _dbContext.Currencies.AddRange(currencyFrom, currencyTo);
        await _dbContext.SaveChangesAsync();

        const decimal commssionValue = 0.14m;
        var body = new CommissionAddModel(currencyFrom.Id.Value, currencyTo.Id.Value, commssionValue);
        string token = await LoginAsAdminAsync();

        // act
        var response = await _httpClient
            .WithBearerToken(token)
            .PostAsJsonAsync(Route, body);

        // assert
        var commissionId = await response.DeserializeToAsync<CommissionId>();
        var commission = await _dbContext.Commissions.SingleOrDefaultAsync(e => e.Id == commissionId);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        commission.Should().NotBeNull();
        commission?.CurrencyFromId.Should().BeEquivalentTo(new CurrencyId(body.CurrencyFromId));
        commission?.CurrencyToId.Should().BeEquivalentTo(new CurrencyId(body.CurrencyToId));
        commission?.Value.Should().Be(body.Value);
    }

    [Fact]
    public async Task AddCommission_Should_Return_Forbidden_Status_Code_when_not_admin_try_to_add_commission()
    {
        // arrange
        Guid randomCurrencyFromId = Guid.NewGuid();
        Guid randomCurrencyToId = Guid.NewGuid();
        const decimal commissionValue = 0.18m;

        var body = new CommissionAddModel(randomCurrencyFromId, randomCurrencyToId, commissionValue);
        string token = await LoginAsUserAsync();

        // act
        var response = await _httpClient
            .WithBearerToken(token)
            .PostAsJsonAsync(Route, body);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task AddCommission_Should_Return_BadRequest_Status_Code_when_invalid_body_passed()
    {
        // arrange
        Guid randomCurrencyFromIdThatNotExistsInDatabase = Guid.NewGuid();
        Guid randomCurrencyToIdThatNotExistsInDatabase = Guid.NewGuid();
        const decimal invalidCommissionValue = Constraints.Commission.MaxCommissionValue + 0.01m;

        var body = new CommissionAddModel(randomCurrencyFromIdThatNotExistsInDatabase, randomCurrencyToIdThatNotExistsInDatabase, invalidCommissionValue);
        string token = await LoginAsAdminAsync();

        // act
        var response = await _httpClient
            .WithBearerToken(token)
            .PostAsJsonAsync(Route, body);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
