using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public record CommissionCreateDTO(CurrencyId CurrencyFromId, CurrencyId CurrencyToId, decimal CommissionValue);