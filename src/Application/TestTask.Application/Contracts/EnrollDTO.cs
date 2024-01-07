using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public record EnrollDTO(CurrencyId CurrencyFromId, decimal MoneyAmount, MoneyAccountId MoneyAccountToId);
