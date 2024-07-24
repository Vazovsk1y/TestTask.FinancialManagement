using TestTask.Domain.Common;

namespace TestTask.Domain.Entities;

public class ExchangeRate : Entity<ExchangeRateId>
{
    public required CurrencyId CurrencyFromId { get; init; }

    public required CurrencyId CurrencyToId { get; init; }

    public required decimal Value { get; set; }

    public required DateTimeOffset UpdatedAt { get; set; }

    public Currency? From { get; init; }

    public Currency? To { get; init; }
}

public record ExchangeRateId(Guid Value) : IValueId<ExchangeRateId>
{
    public static ExchangeRateId Create() => new(Guid.NewGuid());
}