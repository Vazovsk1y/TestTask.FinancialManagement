using FluentValidation;
using TestTask.Domain.Constants;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class CommissionAddModelValidator : AbstractValidator<CommissionAddModel>
{
	public CommissionAddModelValidator()
	{
		RuleFor(e => e.CurrencyToId).NotEmpty().NotEqual(Guid.Empty).NotEqual(e => e.CurrencyFromId);
		RuleFor(e => e.CurrencyFromId).NotEmpty().NotEqual(Guid.Empty).NotEqual(e => e.CurrencyToId);
		RuleFor(e => e.Value).NotEmpty().GreaterThanOrEqualTo(0m).LessThanOrEqualTo(Constraints.Commission.MaxCommissionValue);
	}
}