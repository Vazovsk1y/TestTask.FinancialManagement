using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services;
using TestTask.WebApi.Common;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Controllers;

public class UsersController(IUserService userService) : BaseController
{
	[AllowAnonymous]
	[HttpPost("sign-up")]
	public async Task<IActionResult> RegisterUser(UserRegisterModel registerModel, CancellationToken cancellationToken)
	{
		var dto = registerModel.ToDTO();
		var result = await userService.RegisterAsync(dto, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}
}
