using TestTask.Domain.Constants;
using TestTask.Domain.Entities;

namespace TestTask.DAL.SQLServer.Extensions;

public interface IDatabaseSeeder
{
	void Seed();
}

internal class DatabaseSeeder(TestTaskDbContext dbContext) : IDatabaseSeeder
{
	private const int CurrenciesCount = 5;

	private static readonly IList<Currency> Currencies = Domain.Constants.Currencies
	.Supported
	.Take(CurrenciesCount)
	.Select(e => new Currency
	{
		Title = e.Title,
		AlphabeticCode = e.AlphabeticCode,
		NumericCode = e.NumericCode,
	})
	.ToList();

	private static readonly IReadOnlyCollection<Role> Roles = new string[]
	{
		DefaultRoles.User,
		DefaultRoles.Admin
	}
	.Select(e => new Role
	{
		Title = e
	})
	.ToList();

	private static readonly IReadOnlyCollection<UserSeedModel> UsersSeed = new UserSeedModel[]
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
            for (int i = 0; i < Currencies.Count; i++)
            {
                for (int j = 0; j < Currencies.Count; j++)
                {
                    if (j != i)
					{
						var commission = new Commission
						{
							CurrencyFromId = Currencies[i].Id,
							CurrencyToId = Currencies[j].Id,
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

            for (var i = 0; i < Currencies.Count; i++)
            {
                for (int j = 0; j < Currencies.Count; j++)
                {
                    if (i != j)
					{
						var exchangeRate = new ExchangeRate
						{
							CurrencyFromId = Currencies[i].Id,
							CurrencyToId = Currencies[j].Id,
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

		var users = UsersSeed
		.Select(e => new User
		{
			Email = e.Email,
			FullName = e.FullName,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(e.Password),
		}).ToList();

		var userRoles = new List<UserRole>();
        foreach (var user in users)
        {
			var first = UsersSeed.Single(e => e.Email == user.Email);
			userRoles.AddRange(first.Roles.Select(i => new UserRole { RoleId = Roles.Single(e => e.Title == i).Id, UserId = user.Id }));
        }

		var moneyAccounts = new List<MoneyAccount>();
        foreach (var currency in Currencies)
        {
			moneyAccounts.AddRange(users.Select(e => new MoneyAccount { CurrencyId = currency.Id, Balance = 0m, UserId = e.Id }));
        }

        dbContext.Currencies.AddRange(Currencies);
        dbContext.Roles.AddRange(Roles);
        dbContext.Commissions.AddRange(Commissions);
        dbContext.ExchangeRates.AddRange(ExchangeRates);
        dbContext.Users.AddRange(users);
        dbContext.UserRoles.AddRange(userRoles);
        dbContext.MoneyAccounts.AddRange(moneyAccounts);

		dbContext.SaveChanges();
    }

	private bool IsAbleToSeed()
	{
		return new bool[] 
		{ 
			dbContext.Currencies.Any(),
			dbContext.Roles.Any(),
			dbContext.Commissions.Any(),
			dbContext.ExchangeRates.Any(),
			dbContext.Users.Any(),
			dbContext.MoneyAccounts.Any(),
			dbContext.UserRoles.Any(),
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

