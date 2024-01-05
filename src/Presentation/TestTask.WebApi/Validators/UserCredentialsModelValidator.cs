using FluentValidation;
using TestTask.Domain.Constants;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class UserCredentialsModelValidator : AbstractValidator<UserCredentialsModel>
{
	public UserCredentialsModelValidator()
	{
		RuleFor(e => e.Email).NotEmpty().EmailAddress();
		RuleFor(e => e.Password).NotEmpty().Matches(Constraints.Credentials.PasswordRegex());
	}
}