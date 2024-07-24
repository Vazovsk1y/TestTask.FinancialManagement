using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services;
using TestTask.Domain.Entities;
using TestTask.WebApi.Controllers.Common;
using TestTask.WebApi.Extensions;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Controllers;

public class UsersController(
	IUserService userService,
	IMoneyAccountService moneyAccountService,
	IMoneyOperationService moneyOperationService
	) : BaseController
{
	[HttpPost("sign-up")]
	public async Task<IActionResult> RegisterUser(UserRegisterModel registerModel, CancellationToken cancellationToken)
	{
		var dto = registerModel.ToDTO();
		var result = await userService.RegisterAsync(dto, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}

	[HttpPost("sign-in")]
	public async Task<IActionResult> LoginUser(UserCredentialsModel credentialsModel, CancellationToken cancellationToken)
	{
		var dto = credentialsModel.ToDTO();
		var result = await userService.LoginAsync(dto, cancellationToken);
		return result.IsSuccess ? Ok(result.Value.TokenValue) : BadRequest(result.ErrorMessage);
	}

	[HttpGet("{id}/money-accounts")]
	[Authorize]
	public async Task<IActionResult> GetAssociatedWithUserMoneyAccounts(Guid id, CancellationToken cancellationToken)
	{
		var result = await moneyAccountService.GetAllByUserIdAsync(new UserId(id), cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}

	[HttpGet("{id}/money-operations")]
	[Authorize]
	public async Task<IActionResult> GetAssociatedWithUserMoneyOperations(Guid id, CancellationToken cancellationToken)
	{
		var result = await moneyOperationService.GetAllByUserIdAsync(new UserId(id), cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}
}