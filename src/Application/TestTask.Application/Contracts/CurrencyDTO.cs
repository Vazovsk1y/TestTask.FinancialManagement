using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public record CurrencyDTO(CurrencyId Id, string Title, string NumericCode, string AlphabeticCode);