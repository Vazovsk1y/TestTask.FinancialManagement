using FluentValidation;
using TestTask.Application.Contracts;

namespace TestTask.Application.Implementation.Validators;

public class EnrollDTOValidator : AbstractValidator<EnrollDTO>
{
	public EnrollDTOValidator()
	{
		RuleFor(e => e.MoneyAmount).NotEmpty().GreaterThanOrEqualTo(0.1m);
		RuleFor(e => e.MoneyAccountToId).NotEmpty();
	}
}