using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public record CommissionDTO(CommissionId Id, string CurrencyFrom, string CurrencyTo, decimal Value);