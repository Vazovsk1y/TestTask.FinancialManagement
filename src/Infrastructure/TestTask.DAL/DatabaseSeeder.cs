using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;

namespace TestTask.DAL;

internal static class DatabaseSeeder
{
	private static readonly IReadOnlyCollection<Currency> _currencies = new (string title, string numCode, string alphCode)[]
	{
		DefaultCurrencies.USD,
		DefaultCurrencies.EUR,
		DefaultCurrencies.UAH,
		DefaultCurrencies.GBP,
		DefaultCurrencies.JPY,
		DefaultCurrencies.RUB
	}
	.Select(e => new Currency
	{
		Title = e.title,
		AlphabeticCode = e.alphCode,
		NumericCode = e.numCode,
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

	private static readonly IReadOnlyCollection<Commission> _commissions = new Commission[]
	{
		new() {
			CurrencyFromId = _currencies.Single(e => e.NumericCode == DefaultCurrencies.UAH.NumericCode).Id,
			CurrencyToId = _currencies.Single(e => e.NumericCode == DefaultCurrencies.RUB.NumericCode).Id,
			Value = 0.1m
		},
		new() {
			CurrencyFromId = _currencies.Single(e => e.NumericCode == DefaultCurrencies.RUB.NumericCode).Id,
			CurrencyToId = _currencies.Single(e => e.NumericCode == DefaultCurrencies.UAH.NumericCode).Id,
			Value = 0.21m
		},
		new() {
			CurrencyFromId = _currencies.Single(e => e.NumericCode == DefaultCurrencies.USD.NumericCode).Id,
			CurrencyToId = _currencies.Single(e => e.NumericCode == DefaultCurrencies.UAH.NumericCode).Id,
			Value = 0.14m
		},
		new() {
			CurrencyFromId = _currencies.Single(e => e.NumericCode == DefaultCurrencies.UAH.NumericCode).Id,
			CurrencyToId = _currencies.Single(e => e.NumericCode == DefaultCurrencies.USD.NumericCode).Id,
			Value = 0.08m
		},
		new() {
			CurrencyFromId = _currencies.Single(e => e.NumericCode == DefaultCurrencies.RUB.NumericCode).Id,
			CurrencyToId = _currencies.Single(e => e.NumericCode == DefaultCurrencies.EUR.NumericCode).Id,
			Value = 0.15m
		},
	};

	public static void SeedData(this ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Currency>().HasData(_currencies);
		modelBuilder.Entity<Role>().HasData(_roles);
		modelBuilder.Entity<Commission>().HasData(_commissions);

		var users = _usersSeed
		.Select(e => new User
		{
			Email = e.Email,
			FullName = e.FullName,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(e.Password),
		}).ToList();

		modelBuilder.Entity<User>().HasData(users);

		var userRoles = new List<UserRole>();
        foreach (var user in users)
        {
			var first = _usersSeed.Single(e => e.Email == user.Email);
			userRoles.AddRange(first.Roles.Select(i => new UserRole { RoleId = _roles.Single(e => e.Title == i).Id, UserId = user.Id }));
        }

		modelBuilder.Entity<UserRole>().HasData(userRoles);

		var moneyAccounts = new List<MoneyAccount>();
        foreach (var currency in _currencies)
        {
			moneyAccounts.AddRange(users.Select(e => new MoneyAccount { CurrencyId = currency.Id, Balance = 0m, UserId = e.Id }));
        }

		modelBuilder.Entity<MoneyAccount>().HasData(moneyAccounts);
    }

	private class UserSeedModel
	{
		public required string FullName { get; init; }

		public required string Email { get; init; }

		public required string Password { get; init; }

		public required string[] Roles { get; init; }
	}
}

