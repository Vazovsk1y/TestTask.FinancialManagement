using TestTask.Application.Contracts;
using TestTask.Domain.Entities;
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

	public static EnrollDTO ToDTO(this EnrollModel model)
	{
		return new EnrollDTO(new CurrencyId(model.CurrencyFromId), model.MoneyAmount, new MoneyAccountId(model.MoneyAccountId));
	}

	public static CommissionAddDTO ToDTO(this CommissionAddModel model)
	{
		return new CommissionAddDTO(new CurrencyId(model.CurrencyFromId), new CurrencyId(model.CurrencyToId), model.Value);
	}

	public static WithdrawalDTO ToDTO(this WithdrawalModel model)
	{
		return new WithdrawalDTO(new MoneyAccountId(model.MoneyAccountFromId), model.MoneyAmount);
	}
}