using FluentValidation;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class EnrollModelValidator : AbstractValidator<EnrollModel>
{
	public EnrollModelValidator()
	{
		RuleFor(e => e.MoneyAmount).NotEmpty().GreaterThanOrEqualTo(0.1m);
		RuleFor(e => e.MoneyAccountId).NotEmpty().NotEqual(Guid.Empty);
	}
}