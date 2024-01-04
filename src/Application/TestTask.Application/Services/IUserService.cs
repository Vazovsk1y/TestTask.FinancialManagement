using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Domain.Entities;

namespace TestTask.Application.Services;

public interface IUserService
{
	Task<Result<UserId>> RegisterAsync(UserRegisterDTO registerDTO, CancellationToken cancellationToken = default);

	Task<Result<TokenDTO>> LoginAsync(UserCredentialsDTO credentialsDTO, CancellationToken cancellationToken = default);
}