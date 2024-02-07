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
        RuleFor(e => e).Must(e => Constraints.Currency.IsSupported(e.Title, e.AlphabeticCode, e.NumericCode)).WithMessage("Currency is not supported yet.");
    }
}