using FluentValidation;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class EnrollModelValidator : AbstractValidator<EnrollModel>
{
	public EnrollModelValidator()
	{
		RuleFor(e => e.CurrencyFromId).NotEmpty().NotEqual(Guid.Empty);
		RuleFor(e => e.MoneyAmount).NotEmpty().GreaterThanOrEqualTo(1);
		RuleFor(e => e.MoneyAccountId).NotEmpty().NotEqual(Guid.Empty);
	}
}