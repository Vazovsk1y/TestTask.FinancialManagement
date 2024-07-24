using FluentValidation;
using TestTask.Application.Contracts;

namespace TestTask.Application.Implementation.Validators;

public class TransferDTOValidator : AbstractValidator<TransferDTO>
{
	public TransferDTOValidator()
	{
		RuleFor(e => e.MoneyAccountFromId).NotEmpty().NotEqual(e => e.MoneyAccountToId);
		RuleFor(e => e.MoneyAccountToId).NotEmpty().NotEqual(e => e.MoneyAccountFromId);
		RuleFor(e => e.MoneyAmount).NotEmpty().GreaterThanOrEqualTo(1);
	}
}