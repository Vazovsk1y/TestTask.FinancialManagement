using Microsoft.AspNetCore.Authorization;

namespace TestTask.WebApi.Validators;

public class PermittedToAttribute : AuthorizeAttribute
{
	public PermittedToAttribute(params string[] roles)
	{
		ArgumentNullException.ThrowIfNull(roles);
		foreach (var item in roles)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(item);
		}

		Roles = roles.Length == 0 ? roles[0] : string.Join(',', roles);
	}
}