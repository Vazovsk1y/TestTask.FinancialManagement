using TestTask.Domain.Common;

namespace TestTask.Domain.Entities;

public class Currency : Entity<CurrencyId>
{
	public required string Title { get; set; }

	public required string AlphabeticCode { get; set; }

	public required string NumericCode { get; set; }

	public Currency() : base() { }
}

public record CurrencyId(Guid Value) : IValueId<CurrencyId>
{
	public static CurrencyId Create() => new(Guid.NewGuid());
}