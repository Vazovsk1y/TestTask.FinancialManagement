using TestTask.Domain.Common;

namespace TestTask.Domain.Entities;

public class Currency : Entity<CurrencyId>
{
	public required string Title { get; init; }

	public required string AlphabeticCode { get; init; }

	public required string NumericCode { get; init; }
}

public record CurrencyId(Guid Value) : IValueId<CurrencyId>
{
	public static CurrencyId Create() => new(Guid.NewGuid());
}