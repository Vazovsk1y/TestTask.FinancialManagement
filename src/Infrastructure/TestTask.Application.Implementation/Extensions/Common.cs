using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Extensions;

public static class Common
{
	internal static async Task<Role> GetRoleByTitleAsync(this IQueryable<Role> roles, string roleTitle)
	{
		return await roles.SingleAsync(e => e.Title == roleTitle);
	}

	internal static bool IsInRole(this User user, string roleTitle)
	{
		return user.Roles.Any(e => e.Role!.Title == roleTitle);
	}

	internal static bool IsInRole(this User user, RoleId roleId)
	{
		return user.Roles.Any(e => e.RoleId == roleId);
	}

	internal static async Task<User?> GetById(this IQueryable<User> users, UserId userId)
	{
		return await users.SingleOrDefaultAsync(e => e.Id == userId);
	}
}