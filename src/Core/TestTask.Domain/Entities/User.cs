using TestTask.Domain.Common;

namespace TestTask.Domain.Entities;

public class User : Entity<UserId>
{
	public required string Email { get; init; }

	public required string FullName { get; init; }

	public required string PasswordHash { get; init; }

	public ICollection<MoneyAccount> MoneyAccounts { get; init; } = new HashSet<MoneyAccount>();

	public ICollection<UserRole> Roles { get; init; } = new HashSet<UserRole>();
}

public record UserId(Guid Value) : IValueId<UserId>
{
	public static UserId Create() => new(Guid.NewGuid());
}