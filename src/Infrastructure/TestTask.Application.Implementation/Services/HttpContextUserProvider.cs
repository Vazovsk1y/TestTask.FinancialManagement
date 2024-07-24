using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TestTask.Application.Services;
using TestTask.Application.Shared;
using TestTask.DAL.SQLServer;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

internal class HttpContextUserProvider(
	IHttpContextAccessor httpContextAccessor,
	TestTaskDbContext dbContext) : IUserProvider
{
	public Result<UserId> GetCurrent()
	{
		if (httpContextAccessor.HttpContext.User.Identity is { IsAuthenticated: false })
		{
			return Result.Failure<UserId>(Errors.Auth.Unauthenticated);
		}

		var id = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value;
		if (id is null)
		{
			return Result.Failure<UserId>("Id claim is not defined.");
		}

		if (!Guid.TryParse(id, out var result))
		{
			return Result.Failure<UserId>("Unable parse id to guid.");
		}

		var userId = new UserId(result);
		if (!dbContext.Users.Any(e => e.Id == userId))
		{
			return Result.Failure<UserId>(Errors.EntityWithPassedIdIsNotExists(nameof(User)));
		}

		return userId;
	}
}