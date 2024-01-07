using FluentValidation;
using TestTask.Application.Contracts;

namespace TestTask.Application.Implementation.Validators;

public class WithdrawalDTOValidator : AbstractValidator<WithdrawalDTO>
{
	public WithdrawalDTOValidator()
	{
		RuleFor(e => e.MoneyAccountFromId).NotEmpty();
		RuleFor(e => e.MoneyAmount).NotEmpty().GreaterThanOrEqualTo(1m);
	}
}