using FluentValidation;
using TestTask.Application.Contracts;
using TestTask.Domain.Constants;

namespace TestTask.Application.Implementation.Validators;

public class CurrencyAddDTOValidator : AbstractValidator<CurrencyAddDTO>
{
	public CurrencyAddDTOValidator()
	{
		RuleFor(e => e.Title).NotEmpty().MaximumLength(Constraints.Currency.MaxTitleLength);
		RuleFor(e => e.NumericCode).NotEmpty().Must(Constraints.CurrencyCodes.IsNumericCode);
		RuleFor(e => e.AlphabeticCode).NotEmpty().Must(Constraints.CurrencyCodes.IsAlphabeticCode);
	}
}