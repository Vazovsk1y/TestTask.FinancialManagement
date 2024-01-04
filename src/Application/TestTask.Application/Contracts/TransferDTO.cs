using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public record TransferDTO(MoneyAccountId MoneyAccountFromId, MoneyAccountId MoneyAccountToId, decimal MoneyAmount);