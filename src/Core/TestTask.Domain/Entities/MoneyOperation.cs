using TestTask.Domain.Common;
using TestTask.Domain.Enums;

namespace TestTask.Domain.Entities;

public class MoneyOperation : Entity<MoneyOperationId>
{
	public MoneyAccountId? MoneyAccountFromId { get; init; }

	public MoneyAccountId? MoneyAccountToId { get; init; }

	public required decimal AppliedCommissionValue { get; init; }

	public required decimal AppliedExchangeRate { get; init; }

	public required decimal MoneyAmount { get; init; }

	public required DateTimeOffset OperationDate { get; init; }

	public required MoneyOperationTypes OperationType { get; init; }

	public required MoneyMoveTypes MoveType { get; init; }

	public MoneyOperation() : base() { }
}

public record MoneyOperationId(Guid Value) : IValueId<MoneyOperationId>
{
	public static MoneyOperationId Create() => new(Guid.NewGuid());
}
