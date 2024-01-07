using FluentValidation;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class TransferModelValidator : AbstractValidator<TransferModel>
{
	public TransferModelValidator()
	{
		RuleFor(e => e.MoneyAccountFromId).NotEmpty().NotEqual(Guid.Empty).NotEqual(e => e.MoneyAccountToId);
		RuleFor(e => e.MoneyAccountToId).NotEmpty().NotEqual(Guid.Empty).NotEqual(e => e.MoneyAccountFromId);
		RuleFor(e => e.MoneyAmount).NotEmpty().GreaterThanOrEqualTo(1);
	}
}