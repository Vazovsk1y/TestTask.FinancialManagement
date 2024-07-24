using FluentValidation;
using TestTask.Application.Contracts;

namespace TestTask.Application.Implementation.Validators;

public class EnrollDTOValidator : AbstractValidator<EnrollDTO>
{
	public EnrollDTOValidator()
	{
		RuleFor(e => e.CurrencyFromId).NotEmpty();
		RuleFor(e => e.MoneyAmount).NotEmpty().GreaterThanOrEqualTo(1m);
		RuleFor(e => e.MoneyAccountToId).NotEmpty();
	}
}