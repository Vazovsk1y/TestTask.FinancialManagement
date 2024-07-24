using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Application.Services;
using TestTask.DAL.SQLServer;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

internal class UserService : BaseService, IUserService
{
	private readonly ITokenProvider _tokenProvider;
	public UserService(TestTaskDbContext dbContext, IUserProvider userProvider, IServiceScopeFactory serviceScopeFactory, ITokenProvider tokenProvider) : base(dbContext, userProvider, serviceScopeFactory)
	{
		_tokenProvider = tokenProvider;
	}

	public async Task<Result<TokenDTO>> LoginAsync(UserCredentialsDTO credentialsDTO, CancellationToken cancellationToken = default)
	{
		var validationResult = Validate(credentialsDTO);
		if (validationResult.IsFailure)
		{
			return Result.Failure<TokenDTO>(validationResult.ErrorMessage);
		}

		var user = await _dbContext
			.Users
			.Include(e => e.Roles)
			.ThenInclude(e => e.Role)
			.SingleOrDefaultAsync(e => e.Email == credentialsDTO.Email, cancellationToken);

		if (user is null)
		{
			return Result.Failure<TokenDTO>(Errors.Auth.InvalidCredentials);
		}

		if (!BCrypt.Net.BCrypt.Verify(credentialsDTO.Password, user.PasswordHash))
		{
			return Result.Failure<TokenDTO>(Errors.Auth.InvalidCredentials);
		}

		string token = _tokenProvider.GenerateToken(user);

		return new TokenDTO(token);
	}

	public async Task<Result<UserId>> RegisterAsync(UserRegisterDTO registerDTO, CancellationToken cancellationToken = default)
	{
		var userCreationResult = await CreateUser(registerDTO);
		if (userCreationResult.IsFailure)
		{
			return Result.Failure<UserId>(userCreationResult.ErrorMessage);
		}

		var user = userCreationResult.Value;
		_dbContext.Users.Add(user);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return user.Id;
	}

	private async Task<Result<User>> CreateUser(UserRegisterDTO registerDTO)
	{
		var validationResult = Validate(registerDTO);
		if (validationResult.IsFailure)
		{
			return Result.Failure<User>(validationResult.ErrorMessage);
		}

		if (await IsEmailAlreadyTaken(registerDTO.Credentials.Email))
		{
			return Result.Failure<User>(Errors.EmailIsAlreadyTaken);
		}

		var user = new User
		{
			Email = registerDTO.Credentials.Email,
			FullName = registerDTO.FullName,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Credentials.Password),
		};

		var defaultRole = await _dbContext.Roles.GetRoleByTitleAsync(DefaultRoles.User);
		user.Roles.Add(new UserRole { RoleId = defaultRole.Id, UserId = user.Id });
		return user;
	}

	private async Task<bool> IsEmailAlreadyTaken(string email)
	{
		return await _dbContext.Users.AnyAsync(e => e.Email == email);
	}
}