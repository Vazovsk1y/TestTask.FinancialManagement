using FluentValidation;
using TestTask.WebApi.ViewModels;
using TestTask.Domain.Constants;

namespace TestTask.WebApi.Validators;

public class CurrencyAddModelValidator : AbstractValidator<CurrencyAddModel>
{
	public CurrencyAddModelValidator()
	{
		RuleFor(e => e.Title).NotEmpty().MaximumLength(Constraints.Currency.MaxTitleLength);
		RuleFor(e => e.NumericCode).NotEmpty().Must(Constraints.CurrencyCodes.IsNumericCode);
		RuleFor(e => e.AlphabeticCode).NotEmpty().Must(Constraints.CurrencyCodes.IsAlphabeticCode);
		RuleFor(e => e).Must(e => Constraints.Currency.IsSupported(e.Title, e.AlphabeticCode, e.NumericCode)).WithMessage("Currency is not supported yet.");
	}
}