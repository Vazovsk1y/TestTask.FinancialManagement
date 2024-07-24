namespace TestTask.Domain.Entities;

public class UserRole
{
	public required UserId UserId { get; init; }

	public required RoleId RoleId { get; init; }

	public User? User { get; init; }

	public Role? Role { get; init; }
}