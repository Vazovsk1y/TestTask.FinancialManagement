using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Services;
using TestTask.Application.Shared;
using TestTask.DAL.SQLServer;

namespace TestTask.Application.Implementation.Services;

internal abstract class BaseService(
	TestTaskDbContext dbContext,
	IUserProvider userProvider,
	IServiceScopeFactory serviceScopeFactory)
{
	protected readonly TestTaskDbContext DbContext = dbContext;
	protected readonly IUserProvider UserProvider = userProvider;
	protected readonly IServiceScopeFactory ScopeFactory = serviceScopeFactory;

	protected Result Validate(params object[] objects)
	{
		using var scope = ScopeFactory.CreateScope();
        foreach (var item in objects)
        {
			var itemType = item.GetType();
			var validatorType = typeof(IValidator<>).MakeGenericType(itemType);
			var validator = scope.ServiceProvider.GetRequiredService(validatorType);
			var validationResult = (ValidationResult)validator.GetType().GetMethod(nameof(IValidator.Validate), [itemType])!.Invoke(validator, new[] { item })!;
			if (!validationResult.IsValid)
			{
				return Result.Failure(validationResult.ToString());
			}
		}

		return Result.Success();
    }
}