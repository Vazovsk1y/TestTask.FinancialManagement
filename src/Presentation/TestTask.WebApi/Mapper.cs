using TestTask.Application.Contracts;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi;

public static class Mapper
{
	public static UserCredentialsDTO ToDTO(this UserCredentialsModel model)
	{
		return new UserCredentialsDTO(model.Email, model.Password);
	}

	public static UserRegisterDTO ToDTO(this UserRegisterModel model)
	{
		return new UserRegisterDTO(model.FullName, model.Credentials.ToDTO());
	}

	public static CurrencyAddDTO ToDTO(this CurrencyAddModel model)
	{
		return new CurrencyAddDTO(model.Title, model.NumericCode, model.AlphabeticCode);
	}
}