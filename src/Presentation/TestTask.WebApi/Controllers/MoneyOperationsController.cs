using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services;
using TestTask.WebApi.Common;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Controllers;

[Route("api/money-operations")]
public class MoneyOperationsController(IMoneyOperationService moneyOperationService) : AuthorizeController
{
	[HttpPost("enroll")]
	public async Task<IActionResult> EnrollMoneyAccount(EnrollModel enrollModel, CancellationToken cancellationToken)
	{
		var dto = enrollModel.ToDTO();
		var result = await moneyOperationService.EnrollAsync(dto, cancellationToken);
		return result.IsSuccess ? Created(string.Empty, result.Value) : BadRequest(result.ErrorMessage);
	}

	[HttpPost("withdrawal")]
	public async Task<IActionResult> WithdrawalMoneyAccount(WithdrawalModel withdrawalModel, CancellationToken cancellationToken)
	{
		var dto = withdrawalModel.ToDTO();
		var result = await moneyOperationService.WithdrawalAsync(dto, cancellationToken);
		return result.IsSuccess ? Created(string.Empty, result.Value) : BadRequest(result.ErrorMessage);
	}

	[HttpPost("transfer")]
	public async Task<IActionResult> TransferMoney(TransferModel transfer, CancellationToken cancellationToken)
	{
		var dto = transfer.ToDTO();
		var result = await moneyOperationService.TransferAsync(dto, cancellationToken);
		return result.IsSuccess ? Created(string.Empty, result.Value) : BadRequest(result.ErrorMessage);
	}
}