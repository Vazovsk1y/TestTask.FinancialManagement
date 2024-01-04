using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public record MoneyAccountDTO(MoneyAccountId Id, CurrencyDTO Currency, decimal Balance);