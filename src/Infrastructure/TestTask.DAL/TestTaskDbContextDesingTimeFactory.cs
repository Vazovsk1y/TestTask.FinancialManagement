using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TestTask.DAL;

public class TestTaskDbContextDesingTimeFactory : IDesignTimeDbContextFactory<TestTaskDbContext>
{
	private const string ConnectionString = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CurrecncyExchangeDb;Integrated Security=True;Connect Timeout=30;";

	public TestTaskDbContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder();
		optionsBuilder.UseSqlServer(ConnectionString);
		return new TestTaskDbContext(optionsBuilder.Options);
	}
}