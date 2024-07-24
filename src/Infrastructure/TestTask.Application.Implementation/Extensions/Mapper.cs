using TestTask.Application.Contracts;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Extensions;

public static class Mapper
{
	public static MoneyAccountDTO ToDTO(this MoneyAccount moneyAccount)
	{
		return new MoneyAccountDTO(moneyAccount.Id, moneyAccount.UserId, moneyAccount.Currency!.ToDTO(), moneyAccount.Balance);
	}

	public static CurrencyDTO ToDTO(this Currency currency)
	{
		return new CurrencyDTO(currency.Id, currency.Title, currency.NumericCode, currency.AlphabeticCode);
	}

	public static MoneyOperationDTO ToDTO(this MoneyOperation moneyOperation)
	{
		return new MoneyOperationDTO(
			moneyOperation.Id, 
			moneyOperation.MoneyAccountFromId,
			moneyOperation.MoneyAccountToId,
			moneyOperation.MoveType,
			moneyOperation.OperationType,
			moneyOperation.MoneyAmount,
			moneyOperation.AppliedCommissionValue,
			moneyOperation.AppliedExchangeRate,
			moneyOperation.OperationDate
			);
	}
}