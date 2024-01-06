using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public record MoneyAccountDTO(MoneyAccountId Id, UserId UserId, CurrencyDTO Currency, decimal Balance);