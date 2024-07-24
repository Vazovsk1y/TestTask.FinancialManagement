using Microsoft.EntityFrameworkCore;
using TestTask.DAL.SQLServer;
using TestTask.DAL.SQLServer.Extensions;

namespace TestTask.WebApi;

public static class Extensions
{
	public static void MigrateDatabase(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();
		context.Database.Migrate();
	}

    public static void SeedDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
        seeder.Seed();
    }
}