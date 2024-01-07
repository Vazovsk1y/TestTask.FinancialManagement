using FluentValidation;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class WithdrawalModelValidator : AbstractValidator<WithdrawalModel>
{
	public WithdrawalModelValidator()
	{
		RuleFor(e => e.MoneyAccountFromId).NotEmpty().NotEqual(Guid.Empty);
		RuleFor(e => e.MoneyAmount).NotEmpty().GreaterThanOrEqualTo(1);
	}
}