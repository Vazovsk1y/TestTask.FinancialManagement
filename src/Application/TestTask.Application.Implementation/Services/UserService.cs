using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Application.Services;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

internal class UserService : IUserService
{
	public Task<Result<TokenDTO>> LoginAsync(UserCredentialsDTO credentialsDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result<UserId>> RegisterAsync(UserRegisterDTO registerDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
