using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public record EnrollDTO(decimal MoneyAmount, MoneyAccountId MoneyAccountToId);
