using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public record CommissionAddDTO(CurrencyId CurrencyFromId, CurrencyId CurrencyToId, decimal CommissionValue);