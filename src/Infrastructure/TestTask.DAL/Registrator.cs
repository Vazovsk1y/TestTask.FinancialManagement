using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestTask.DAL;

public static class Registrator
{
	public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration) => services
		.AddDbContext<TestTaskDbContext>(e => e.UseSqlServer(configuration.GetConnectionString("Default")));
}