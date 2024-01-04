using TestTask.Domain.Common;

namespace TestTask.Domain.Entities;

public class Commission : Entity<CommissionId>
{
	public const decimal DefaultValue = 0.05m;

	public required CurrencyId CurrencyFromId { get; init; }

	public required Currency CurrencyToId { get; init; }

	public required decimal Value { get; init; }

	private Commission() : base() { }
}

public record CommissionId(Guid Value) : IValueId<CommissionId>
{
	public static CommissionId Create() => new(Guid.NewGuid());
}