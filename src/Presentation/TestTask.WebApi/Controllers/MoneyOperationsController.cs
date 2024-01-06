using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services;
using TestTask.WebApi.Common;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Controllers;

[Route("api/money-operations")]
public class MoneyOperationsController(IMoneyOperationService moneyOperationService) : BaseController
{
	[HttpPost("enroll")]
	public async Task<IActionResult> EnrollMoneyAccount(EnrollModel enrollModel, CancellationToken cancellationToken)
	{
		var dto = enrollModel.ToDTO();
		var result = await moneyOperationService.EnrollAsync(dto, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}
}