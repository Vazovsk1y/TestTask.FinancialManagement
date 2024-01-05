using Microsoft.EntityFrameworkCore;
using TestTask.DAL;

namespace TestTask.WebApi;

public static class Extensions
{
	public static void MigrateDatabase(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();
		context.Database.Migrate();
	}
}