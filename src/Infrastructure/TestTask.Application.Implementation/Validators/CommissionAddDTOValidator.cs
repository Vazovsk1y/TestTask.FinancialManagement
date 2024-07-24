using FluentValidation;
using TestTask.Application.Contracts;

namespace TestTask.Application.Implementation.Validators;

public class CommissionAddDTOValidator : AbstractValidator<CommissionAddDTO>
{
	public CommissionAddDTOValidator()
	{
		RuleFor(e => e.CurrencyToId).NotEmpty().NotEqual(e => e.CurrencyFromId);
		RuleFor(e => e.CurrencyFromId).NotEmpty().NotEqual(e => e.CurrencyToId);
		RuleFor(e => e.CommissionValue).NotEmpty().GreaterThanOrEqualTo(0m).LessThanOrEqualTo(Domain.Constants.Constraints.Commission.MaxCommissionValue);
	}
}