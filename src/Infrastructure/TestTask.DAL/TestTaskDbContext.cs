using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;

namespace TestTask.DAL;

public class TestTaskDbContext(DbContextOptions contextOptions) : DbContext(contextOptions)
{
	public DbSet<User> Users { get; set; }

	public DbSet<Role> Roles { get; set; }

	public DbSet<UserRole> UserRoles { get; set; }

	public DbSet<Commission> Commissions { get; set; }

	public DbSet<MoneyAccount> MoneyAccounts { get; set; }

	public DbSet<MoneyOperation> MoneyOperations { get; set; }

	public DbSet<Currency> Currencies { get; set; }

	public DbSet<ExchangeRate> ExchangeRates { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestTaskDbContext).Assembly);
		modelBuilder.SeedData();
	}
}