using TestTask.Domain.Common;

namespace TestTask.Domain.Entities;

public class Commission : Entity<CommissionId>
{
	public const decimal DefaultValue = 0.05m;

	public required CurrencyId CurrencyFromId { get; init; }

	public required CurrencyId CurrencyToId { get; init; }

	public required decimal Value { get; init; }

	public Currency? From { get; init; }

	public Currency? To { get; init; }
}

public record CommissionId(Guid Value) : IValueId<CommissionId>
{
	public static CommissionId Create() => new(Guid.NewGuid());
}