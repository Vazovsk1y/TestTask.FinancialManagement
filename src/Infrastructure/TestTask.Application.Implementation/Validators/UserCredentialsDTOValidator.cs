using FluentValidation;
using TestTask.Application.Contracts;
using TestTask.Domain.Constants;

namespace TestTask.Application.Implementation.Validators;

public class UserCredentialsDTOValidator : AbstractValidator<UserCredentialsDTO>
{
	public UserCredentialsDTOValidator()
	{
		RuleFor(e => e.Email)
			.NotEmpty()
			.EmailAddress();

		RuleFor(e => e.Password)
			.NotEmpty()
			.WithMessage($"Password property is required.")
			.Matches(Constraints.Credentials.PasswordRegex());
	}
}