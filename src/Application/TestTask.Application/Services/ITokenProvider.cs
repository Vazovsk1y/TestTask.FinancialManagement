using TestTask.Domain.Entities;

namespace TestTask.Application.Services;

public interface ITokenProvider
{
	string GenerateToken(User user);
}