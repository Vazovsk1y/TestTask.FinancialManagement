using TestTask.Application.Shared;
using TestTask.Domain.Entities;

namespace TestTask.Application.Services;

public interface IUserProvider
{
	Result<UserId> GetCurrent();
}