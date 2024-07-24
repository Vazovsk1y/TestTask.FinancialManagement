using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Common;
using TestTask.Application.Services;
using TestTask.DAL.SQLServer;

namespace TestTask.Application.Implementation.Services;

internal abstract class BaseService
{
	protected readonly TestTaskDbContext _dbContext;
	protected readonly IUserProvider _userProvider;
	protected readonly IServiceScopeFactory _scopeFactory;

	protected BaseService(
	TestTaskDbContext dbContext,
	IUserProvider userProvider,
	IServiceScopeFactory serviceScopeFactory)
	{
		_dbContext = dbContext;
		_userProvider = userProvider;
		_scopeFactory = serviceScopeFactory;
	}
	protected Result Validate(params object[] objects)
	{
		using var scope = _scopeFactory.CreateScope();
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