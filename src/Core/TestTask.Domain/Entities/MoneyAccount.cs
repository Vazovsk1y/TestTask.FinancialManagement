using TestTask.Domain.Common;

namespace TestTask.Domain.Entities;

public class MoneyAccount : Entity<MoneyAccountId>
{
	public required UserId UserId { get; init; }

	public required CurrencyId CurrencyId { get; init; }

	public required decimal Balance { get; set; }

	public MoneyAccount() : base() { }
}

public record MoneyAccountId(Guid Value) : IValueId<MoneyAccountId>
{
	public static MoneyAccountId Create() => new(Guid.NewGuid());
}