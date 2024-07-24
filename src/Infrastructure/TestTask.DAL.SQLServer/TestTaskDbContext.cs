using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;

namespace TestTask.DAL.SQLServer;

public class TestTaskDbContext(DbContextOptions contextOptions) : DbContext(contextOptions)
{
	public DbSet<User> Users { get; init; }

	public DbSet<Role> Roles { get; init; }

	public DbSet<UserRole> UserRoles { get; init; }

	public DbSet<Commission> Commissions { get; init; }

	public DbSet<MoneyAccount> MoneyAccounts { get; init; }

	public DbSet<MoneyOperation> MoneyOperations { get; init; }

	public DbSet<Currency> Currencies { get; init; }

	public DbSet<ExchangeRate> ExchangeRates { get; init; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestTaskDbContext).Assembly);
	}
}