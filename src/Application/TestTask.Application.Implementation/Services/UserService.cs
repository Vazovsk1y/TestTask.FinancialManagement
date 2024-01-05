using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Application.Services;
using TestTask.DAL;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

internal class UserService(
	TestTaskDbContext dbContext,
	IValidator<UserRegisterDTO> userRegisterDtoValidator) : IUserService
{
	public Task<Result<TokenDTO>> LoginAsync(UserCredentialsDTO credentialsDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public async Task<Result<UserId>> RegisterAsync(UserRegisterDTO registerDTO, CancellationToken cancellationToken = default)
	{
		var userCreationResult = await CreateUser(registerDTO);
		if (userCreationResult.IsFailure)
		{
			return Result.Failure<UserId>(userCreationResult.ErrorMessage);
		}

		var user = userCreationResult.Value;
		dbContext.Users.Add(user);
		await dbContext.SaveChangesAsync(cancellationToken);
		return user.Id;
	}

	private async Task<Result<User>> CreateUser(UserRegisterDTO registerDTO)
	{
		var validationResult = userRegisterDtoValidator.Validate(registerDTO);
		if (!validationResult.IsValid)
		{
			return Result.Failure<User>(validationResult.ToString());
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

		var defaultRole = await dbContext.Roles.GetRoleByTitleAsync(DefaultRoles.User);
		user.Roles.Add(new UserRole { RoleId = defaultRole.Id, UserId = user.Id });
		return user;
	}

	private async Task<bool> IsEmailAlreadyTaken(string email)
	{
		return await dbContext.Users.AnyAsync(e => e.Email == email);
	}
}
