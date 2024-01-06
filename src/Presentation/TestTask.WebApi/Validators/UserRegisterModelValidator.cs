using FluentValidation;
using TestTask.Application.Contracts;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Validators;

public class UserRegisterModelValidator : AbstractValidator<UserRegisterModel>
{
	public UserRegisterModelValidator(IValidator<UserCredentialsModel> validator)
	{
		RuleFor(e => e.FullName).NotEmpty();
		RuleFor(e => e.Credentials).SetValidator(validator);
	}
}