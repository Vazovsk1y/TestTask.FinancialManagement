using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using TestTask.Application.Contracts;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;
using TestTask.Domain.Enums;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.IntegrationTests.Controllers;

public class MoneyOperationsControllerTests : IntegrationTest
{
    private const string Route = "api/money-operations";
    public MoneyOperationsControllerTests(TestTaskWebApiApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task EnrollMoneyAccount_Should_Add_EnrolmentOperation_To_Database_AND_Return_Created_Status_Code_when_valid_body_passed()
    {
        // arrange
        var (Title, AlphabeticCode, NumericCode) = Currencies.Supported[45];
        var randomValidCurrency = new Currency
        {
            AlphabeticCode = AlphabeticCode,
            NumericCode = NumericCode,
            Title = Title,
        };

        var moneyAccount = new MoneyAccount
        {
            Balance = 0m,
            CurrencyId = randomValidCurrency.Id,
            UserId = User.Id,
        };

        _dbContext.Currencies.Add(randomValidCurrency);
        _dbContext.MoneyAccounts.Add(moneyAccount);
        await _dbContext.SaveChangesAsync();

        const decimal moneyAmount = 1234m;
        var body = new EnrollModel(randomValidCurrency.Id.Value, moneyAmount, moneyAccount.Id.Value);
        string token = await LoginAsUserAsync();

        // act
        var response = await _httpClient
            .WithBearerToken(token)
            .PostAsJsonAsync($"{Route}/enroll", body);

        // assert
        var moneyOperationId = await response.DeserializeToAsync<MoneyOperationId>();
        var moneyOperation = await _dbContext.MoneyOperations.SingleOrDefaultAsync(e => e.Id == moneyOperationId);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        moneyOperation.Should().NotBeNull();
        moneyOperation?.MoneyAccountToId.Should().Be(moneyAccount.Id);
        moneyOperation?.MoveType.Should().Be(MoneyMoveTypes.Adding);
        moneyOperation?.OperationType.Should().Be(MoneyOperationTypes.Enrolment);
    }

    [Fact]
    public async Task EnrollMoneyAccount_Should_Return_Unauthorized_Status_Code_when_unauthenticated_user_try_to_make_enrolment()
    {
        // arrange
        const decimal moneyAmount = 23901m;
        Guid randomCurrencyFromIdThatNotExistsInDatabase = Guid.NewGuid();
        Guid randomMoneyAccountIdThatNotExistsInDatabase = Guid.NewGuid();
        var body = new EnrollModel(randomCurrencyFromIdThatNotExistsInDatabase, moneyAmount, randomMoneyAccountIdThatNotExistsInDatabase);

        // act
        var response = await _httpClient
            .PostAsJsonAsync($"{Route}/enroll", body);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task EnrollMoneyAccount_Should_Return_BadRequest_Status_Code_when_invalid_body_passed()
    {
        // arrange
        const decimal moneyAmount = 23901m;
        Guid randomCurrencyFromIdThatNotExistsInDatabase = Guid.NewGuid();
        Guid randomMoneyAccountIdThatNotExistsInDatabase = Guid.NewGuid();
        var body = new EnrollModel(randomCurrencyFromIdThatNotExistsInDatabase, moneyAmount, randomMoneyAccountIdThatNotExistsInDatabase);
        string token = await LoginAsUserAsync();

        // act
        var response = await _httpClient
            .WithBearerToken(token)
            .PostAsJsonAsync($"{Route}/enroll", body);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task WithdrawalMoneyAccount_Should_Add_WithdrawalOperation_To_Database_AND_Return_Created_Status_Code_when_valid_body_passed()
    {
        // arrange
        var (Title, AlphabeticCode, NumericCode) = Currencies.Supported[33];
        var randomValidCurrency = new Currency
        {
            AlphabeticCode = AlphabeticCode,
            NumericCode = NumericCode,
            Title = Title,
        };

        var moneyAccount = new MoneyAccount
        {
            Balance = 50000m,
            CurrencyId = randomValidCurrency.Id,
            UserId = User.Id,
        };

        _dbContext.Currencies.Add(randomValidCurrency);
        _dbContext.MoneyAccounts.Add(moneyAccount);
        await _dbContext.SaveChangesAsync();

        const decimal moneyAmount = 1234m;
        var body = new WithdrawalModel(moneyAccount.Id.Value, moneyAmount);
        string token = await LoginAsUserAsync();

        // act
        var response = await _httpClient
            .WithBearerToken(token)
            .PostAsJsonAsync($"{Route}/withdrawal", body);

        // assert
        var moneyOperationId = await response.DeserializeToAsync<MoneyOperationId>();
        var moneyOperation = await _dbContext.MoneyOperations.SingleOrDefaultAsync(e => e.Id == moneyOperationId);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        moneyOperation.Should().NotBeNull();
        moneyOperation?.MoneyAccountFromId.Should().Be(moneyAccount.Id);
        moneyOperation?.MoveType.Should().Be(MoneyMoveTypes.Subtracting);
        moneyOperation?.OperationType.Should().Be(MoneyOperationTypes.Withdrawal);
    }

    [Fact]
    public async Task WithdrawalMoneyAccount_Should_Return_Unauthorized_Status_Code_when_unauthenticated_user_try_to_make_enrolment()
    {
        // arrange
        const decimal moneyAmount = 2050m;
        Guid randomCurrencyFromIdThatNotExistsInDatabase = Guid.NewGuid();
        var body = new WithdrawalModel(randomCurrencyFromIdThatNotExistsInDatabase, moneyAmount);

        // act
        var response = await _httpClient
            .PostAsJsonAsync($"{Route}/withdrawal", body);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task WithdrawalMoneyAccount_Should_Return_BadRequest_Status_Code_when_invalid_body_passed()
    {
        // arrange
        const decimal moneyAmount = 2050m;
        Guid randomCurrencyFromIdThatNotExistsInDatabase = Guid.NewGuid();
        var body = new WithdrawalModel(randomCurrencyFromIdThatNotExistsInDatabase, moneyAmount);
        string token = await LoginAsUserAsync();

        // act
        var response = await _httpClient
            .WithBearerToken(token)
            .PostAsJsonAsync($"{Route}/withdrawal", body);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Transfer_Should_Add_Two_Operations_To_Database_AND_Return_Created_Status_Code_when_valid_body_passed()
    {
        // arrange
        var (Title, AlphabeticCode, NumericCode) = Currencies.Supported[60];
        var randomValidCurrency = new Currency
        {
            AlphabeticCode = AlphabeticCode,
            NumericCode = NumericCode,
            Title = Title,
        };

        var moneyAccountFrom = new MoneyAccount
        {
            Balance = 50000m,
            CurrencyId = randomValidCurrency.Id,
            UserId = User.Id,
        };

        var moneyAccountTo = new MoneyAccount
        {
            Balance = 0m,
            CurrencyId = randomValidCurrency.Id,
            UserId = User.Id,
        };

        _dbContext.Currencies.Add(randomValidCurrency);
        _dbContext.MoneyAccounts.AddRange(moneyAccountFrom, moneyAccountTo);
        await _dbContext.SaveChangesAsync();

        const decimal moneyAmount = 1500m;
        var body = new TransferModel(moneyAccountFrom.Id.Value, moneyAccountTo.Id.Value, moneyAmount);
        string token = await LoginAsUserAsync();

        // act
        var response = await _httpClient
            .WithBearerToken(token)
            .PostAsJsonAsync($"{Route}/transfer", body);

        // assert
        var dto = await response.DeserializeToAsync<TransferResponseDTO>();
        var add = await _dbContext.MoneyOperations.SingleOrDefaultAsync(e => e.Id == dto!.Add);
        var sub = await _dbContext.MoneyOperations.SingleOrDefaultAsync(e => e.Id == dto!.Sub);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        sub.Should().NotBeNull();
        add.Should().NotBeNull();

        add?.MoneyAccountFromId.Should().Be(moneyAccountFrom.Id);
        add?.MoneyAccountToId.Should().Be(moneyAccountTo.Id);
        add?.MoveType.Should().Be(MoneyMoveTypes.Adding);
        add?.OperationType.Should().Be(MoneyOperationTypes.Transfer);

        sub?.MoneyAccountFromId.Should().Be(moneyAccountFrom.Id);
        sub?.MoneyAccountToId.Should().Be(moneyAccountTo.Id);
        sub?.MoveType.Should().Be(MoneyMoveTypes.Subtracting);
        sub?.OperationType.Should().Be(MoneyOperationTypes.Transfer);
    }

    [Fact]
    public async Task Transfer_Should_Return_Unauthorized_Status_Code_when_unauthenticated_user_try_to_make_enrolment()
    {
        // arrange
        Guid randomCurrencyFromIdThatNotExistsInDatabase = Guid.NewGuid();
        Guid randomCurrencyToIdThatNotExistsInDatabase = Guid.NewGuid();
        const decimal moneyAmount = 2050m;

        var body = new TransferModel(randomCurrencyFromIdThatNotExistsInDatabase, randomCurrencyToIdThatNotExistsInDatabase, moneyAmount);

        // act
        var response = await _httpClient
            .PostAsJsonAsync($"{Route}/transfer", body);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Transfer_Should_Return_BadRequest_Status_Code_when_invalid_body_passed()
    {
        // arrange
        Guid randomCurrencyFromIdThatNotExistsInDatabase = Guid.NewGuid();
        Guid randomCurrencyToIdThatNotExistsInDatabase = Guid.NewGuid();
        const decimal moneyAmount = 2050m;

        var body = new TransferModel(randomCurrencyFromIdThatNotExistsInDatabase, randomCurrencyToIdThatNotExistsInDatabase, moneyAmount);
        string token = await LoginAsUserAsync();

        // act
        var response = await _httpClient
            .WithBearerToken(token)
            .PostAsJsonAsync($"{Route}/transfer", body);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
