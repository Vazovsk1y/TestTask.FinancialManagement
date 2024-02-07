using TestTask.Domain.Common;

namespace TestTask.Domain.Entities;

public class ExchangeRate : Entity<ExchangeRateId>
{
    public required CurrencyId CurrencyFromId { get; init; }

    public required CurrencyId CurrencyToId { get; init; }

    public required decimal Value { get; set; }

    public required DateTimeOffset UpdatedAt { get; set; }

    public Currency? From { get; set; }

    public Currency? To { get; set; }

    public ExchangeRate() : base() { }
}

public record ExchangeRateId(Guid Value) : IValueId<ExchangeRateId>
{
    public static ExchangeRateId Create() => new(Guid.NewGuid());
}