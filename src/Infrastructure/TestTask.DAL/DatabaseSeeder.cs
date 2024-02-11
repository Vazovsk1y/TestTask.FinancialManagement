using TestTask.Domain.Constants;
using TestTask.Domain.Entities;

namespace TestTask.DAL;

public interface IDatabaseSeeder
{
	void Seed();
}

internal class DatabaseSeeder(TestTaskDbContext dbContext) : IDatabaseSeeder
{
	private const int CurrenciesCount = 5;
	private readonly TestTaskDbContext _dbContext = dbContext;

    private static readonly IList<Currency> _currencies = Currencies
	.Supported
	.Take(CurrenciesCount)
	.Select(e => new Currency
	{
		Title = e.Title,
		AlphabeticCode = e.AlphabeticCode,
		NumericCode = e.NumericCode,
	})
	.ToList();

	private static readonly IReadOnlyCollection<Role> _roles = new string[]
	{
		DefaultRoles.User,
		DefaultRoles.Admin
	}
	.Select(e => new Role
	{
		Title = e
	})
	.ToList();

	private static readonly IReadOnlyCollection<UserSeedModel> _usersSeed = new UserSeedModel[]
	{
		new()
		{
			FullName = "Mike Vazovskiy",
			Email = "penis@gmail.com",
			Password = "penisPopka228#",
			Roles = [DefaultRoles.User, DefaultRoles.Admin] 
		},
		new()
		{
			FullName = "John Doe",
			Email = "popka@gmail.com",
			Password = "penisPopka228#",
			Roles = [DefaultRoles.User]
		}
	};

    private static IReadOnlyCollection<Commission> Commissions
    {
        get
        {
            var result = new List<Commission>();
            for (int i = 0; i < _currencies.Count; i++)
            {
                for (int j = 0; j < _currencies.Count; j++)
                {
                    if (j != i)
					{
						var commission = new Commission
						{
							CurrencyFromId = _currencies[i].Id,
							CurrencyToId = _currencies[j].Id,
							Value = (decimal)(Random.Shared.NextDouble() * (10.0 - 0.1) + 0.1),
                        };

						result.Add(commission);
					}
                }
            }

            return result;
        }
    }

    private static IReadOnlyCollection<ExchangeRate> ExchangeRates 
	{ 
		get 
		{
			var result = new List<ExchangeRate>();
			var date = DateTimeOffset.UtcNow;

            for (int i = 0; i < _currencies.Count; i++)
            {
                for (int j = 0; j < _currencies.Count; j++)
                {
                    if (i != j)
					{
						var exchangeRate = new ExchangeRate
						{
							CurrencyFromId = _currencies[i].Id,
							CurrencyToId = _currencies[j].Id,
							Value = (decimal)(Random.Shared.NextDouble() * (10.0 - 0.1) + 0.1),
							UpdatedAt = date,
						};

						result.Add(exchangeRate);
					}
                }
            }

            return result;
	    }
	}

	public void Seed()
	{
		if (!IsAbleToSeed())
		{
			return;
		}

		var users = _usersSeed
		.Select(e => new User
		{
			Email = e.Email,
			FullName = e.FullName,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(e.Password),
		}).ToList();

		var userRoles = new List<UserRole>();
        foreach (var user in users)
        {
			var first = _usersSeed.Single(e => e.Email == user.Email);
			userRoles.AddRange(first.Roles.Select(i => new UserRole { RoleId = _roles.Single(e => e.Title == i).Id, UserId = user.Id }));
        }

		var moneyAccounts = new List<MoneyAccount>();
        foreach (var currency in _currencies)
        {
			moneyAccounts.AddRange(users.Select(e => new MoneyAccount { CurrencyId = currency.Id, Balance = 0m, UserId = e.Id }));
        }

        _dbContext.Currencies.AddRange(_currencies);
        _dbContext.Roles.AddRange(_roles);
        _dbContext.Commissions.AddRange(Commissions);
        _dbContext.ExchangeRates.AddRange(ExchangeRates);
        _dbContext.Users.AddRange(users);
        _dbContext.UserRoles.AddRange(userRoles);
        _dbContext.MoneyAccounts.AddRange(moneyAccounts);

		_dbContext.SaveChanges();
    }

	private bool IsAbleToSeed()
	{
		return new bool[] 
		{ 
			_dbContext.Currencies.Any(),
			_dbContext.Roles.Any(),
			_dbContext.Commissions.Any(),
			_dbContext.ExchangeRates.Any(),
			_dbContext.Users.Any(),
			_dbContext.MoneyAccounts.Any(),
			_dbContext.UserRoles.Any(),
		}
		.All(e => e is false);
	}

	private class UserSeedModel
	{
		public required string FullName { get; init; }

		public required string Email { get; init; }

		public required string Password { get; init; }

		public required string[] Roles { get; init; }
	}
}

