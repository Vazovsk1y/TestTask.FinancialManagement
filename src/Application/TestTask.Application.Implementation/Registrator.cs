using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Implementation.Services;
using TestTask.Application.Implementation.Validators;
using TestTask.Application.Services;

namespace TestTask.Application.Implementation;

public static class Registrator
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services) => services
		.AddScoped<IUserService, UserService>()
		.AddScoped<IMoneyAccountService, MoneyAccountService>()
		.AddScoped<IMoneyOperationService,  MoneyOperationService>()
		.AddScoped<ICurrencyService, CurrencyService>()
		.AddScoped<ICommissionService, CommissionService>()
		.AddScoped<ITokenProvider, JwtTokenProvider>()
		.AddValidatorsFromAssembly(typeof(UserCredentialsDTOValidator).Assembly)
		;
}