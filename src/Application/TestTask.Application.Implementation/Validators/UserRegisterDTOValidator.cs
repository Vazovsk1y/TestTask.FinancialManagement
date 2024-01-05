using FluentValidation;
using TestTask.Application.Contracts;

namespace TestTask.Application.Implementation.Validators;

public class UserRegisterDTOValidator : AbstractValidator<UserRegisterDTO>
{
	public UserRegisterDTOValidator(IValidator<UserCredentialsDTO> userCredentialsDTOValidator)
	{
		RuleFor(e => e.FullName).NotEmpty().WithMessage($"Full name property is required.");
		RuleFor(e => e.Credentials).SetValidator(userCredentialsDTOValidator);
	}
}