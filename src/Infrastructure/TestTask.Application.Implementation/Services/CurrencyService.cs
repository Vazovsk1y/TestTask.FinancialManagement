using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Contracts;
using TestTask.Application.Services;
using TestTask.Application.Shared;
using TestTask.DAL.SQLServer;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

internal class CurrencyService : BaseService, ICurrencyService
{
	private readonly IClock _clock;
    public CurrencyService(
        TestTaskDbContext dbContext,
        IUserProvider userProvider,
        IServiceScopeFactory serviceScopeFactory,
        IClock clock) : base(dbContext, userProvider, serviceScopeFactory)
    {
        _clock = clock;
    }

    public async Task<Result<CurrencyId>> AddAsync(CurrencyAddDTO currencyAddDTO, CancellationToken cancellationToken = default)
	{
		var requesterIdResult = _userProvider.GetCurrent();
		if (requesterIdResult.IsFailure)
		{
			return Result.Failure<CurrencyId>(requesterIdResult.ErrorMessage);
		}

		var requester = await _dbContext
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

		var currencies = await _dbContext.Currencies.Select(c => new { c.Id, c.AlphabeticCode }).ToListAsync(cancellationToken);
		var date = _clock.GetUtcNow();

		// TODO: use exchange rate provider instead of genering random values.
		// now i'm using random values because i have limited free month calls to the external api.

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

        _dbContext.Currencies.Add(currency);
		_dbContext.ExchangeRates.AddRange(exchangeRates);

		await _dbContext.SaveChangesAsync(cancellationToken);
		return currency.Id;
	}

	public async Task<Result<IReadOnlyCollection<CurrencyDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		var result = await _dbContext.Currencies.Select(e => e.ToDTO()).ToListAsync(cancellationToken);
		return result;
	}

	private async Task<Result<Currency>> CreateCurrency(CurrencyAddDTO currencyAddDTO)
	{
		var validationResult = Validate(currencyAddDTO);
		if (validationResult.IsFailure)
		{
			return Result.Failure<Currency>(validationResult.ErrorMessage);
		}

		var currency = await _dbContext
			.Currencies
			.SingleOrDefaultAsync(e => 
			e.NumericCode == currencyAddDTO.NumericCode 
		|| e.AlphabeticCode == currencyAddDTO.AlphabeticCode 
		|| e.Title == currencyAddDTO.Title);

		return currency switch
		{
			{ } when currency.Title == currencyAddDTO.Title => Result.Failure<Currency>(Errors.EntityWithPropertyIsAlreadyExists(nameof(Currency), "title")),
			{ } when currency.AlphabeticCode == currencyAddDTO.AlphabeticCode => Result.Failure<Currency>(Errors.EntityWithPropertyIsAlreadyExists(nameof(Currency), "alphabetic code")),
			{ } when currency.NumericCode == currencyAddDTO.NumericCode => Result.Failure<Currency>(Errors.EntityWithPropertyIsAlreadyExists(nameof(Currency), "numeric code")),
			null => Result.Success(new Currency { AlphabeticCode = currencyAddDTO.AlphabeticCode, Title = currencyAddDTO.Title, NumericCode = currencyAddDTO.NumericCode }),
			_ => throw new KeyNotFoundException(),
		};
	}
}