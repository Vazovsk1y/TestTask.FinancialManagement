using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public record WithdrawalDTO(MoneyAccountId MoneyAccountFromId, decimal MoneyAmount);