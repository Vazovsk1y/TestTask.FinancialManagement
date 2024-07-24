using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Contracts;
using TestTask.Application.Implementation.Constants;
using TestTask.Application.Implementation.Extensions;
using TestTask.Application.Services;
using TestTask.Application.Shared;
using TestTask.DAL.SQLServer;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

internal class CurrencyService(
	TestTaskDbContext dbContext,
	IUserProvider userProvider,
	IServiceScopeFactory serviceScopeFactory,
	IClock clock)
	: BaseService(dbContext, userProvider, serviceScopeFactory), ICurrencyService
{
	public async Task<Result<CurrencyId>> AddAsync(CurrencyAddDTO currencyAddDTO, CancellationToken cancellationToken = default)
	{
		var requesterIdResult = UserProvider.GetCurrent();
		if (requesterIdResult.IsFailure)
		{
			return Result.Failure<CurrencyId>(requesterIdResult.ErrorMessage);
		}

		var requester = await DbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.GetById(requesterIdResult.Value);

		var actionPermitted = requester!.IsInRole(DefaultRoles.Admin);
		if (!actionPermitted)
		{
			return Result.Failure<CurrencyId>(Errors.Auth.AccessDenied);
		}

		var currencyCreationResult = await CreateCurrency(currencyAddDTO);
		if (currencyCreationResult.IsFailure)
		{
			return Result.Failure<CurrencyId>(currencyCreationResult.ErrorMessage);
		}

		var currency = currencyCreationResult.Value;

		var currencies = await DbContext.Currencies.Select(c => new { c.Id, c.AlphabeticCode }).ToListAsync(cancellationToken);
		var date = clock.GetUtcNow();

		// TODO: use exchange rate provider instead of genering random values.
		// Now i'm using random values because i have limited free month calls to the external api.

        var exchangeRates = currencies
            .Select(e => new ExchangeRate
            {
                CurrencyFromId = currency.Id,
                CurrencyToId = e.Id,
                UpdatedAt = date,
                Value = (decimal)(Random.Shared.NextDouble() * (10.0 - 0.1) + 0.1),
            })
            .Union(currencies.Select(e => new ExchangeRate
            {
                CurrencyFromId = e.Id,
                CurrencyToId = currency.Id,
                UpdatedAt = date,
                Value = (decimal)(Random.Shared.NextDouble() * (10.0 - 0.1) + 0.1),
            }));

        DbContext.Currencies.Add(currency);
		DbContext.ExchangeRates.AddRange(exchangeRates);

		await DbContext.SaveChangesAsync(cancellationToken);
		return currency.Id;
	}

	public async Task<Result<IReadOnlyCollection<CurrencyDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		var result = await DbContext.Currencies.Select(e => e.ToDTO()).ToListAsync(cancellationToken);
		return result;
	}

	private async Task<Result<Currency>> CreateCurrency(CurrencyAddDTO currencyAddDTO)
	{
		var validationResult = Validate(currencyAddDTO);
		if (validationResult.IsFailure)
		{
			return Result.Failure<Currency>(validationResult.ErrorMessage);
		}

		var currency = await DbContext
			.Currencies
			.SingleOrDefaultAsync(e => 
			e.NumericCode == currencyAddDTO.NumericCode 
		|| e.AlphabeticCode == currencyAddDTO.AlphabeticCode 
		|| e.Title == currencyAddDTO.Title);

		return currency switch
		{
			not null when currency.Title == currencyAddDTO.Title => Result.Failure<Currency>(Errors.EntityWithPropertyIsAlreadyExists(nameof(Currency), "title")),
			not null when currency.AlphabeticCode == currencyAddDTO.AlphabeticCode => Result.Failure<Currency>(Errors.EntityWithPropertyIsAlreadyExists(nameof(Currency), "alphabetic code")),
			not null when currency.NumericCode == currencyAddDTO.NumericCode => Result.Failure<Currency>(Errors.EntityWithPropertyIsAlreadyExists(nameof(Currency), "numeric code")),
			null => Result.Success(new Currency { AlphabeticCode = currencyAddDTO.AlphabeticCode, Title = currencyAddDTO.Title, NumericCode = currencyAddDTO.NumericCode }),
			_ => throw new KeyNotFoundException(),
		};
	}
}