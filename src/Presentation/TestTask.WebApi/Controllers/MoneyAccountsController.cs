using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services;
using TestTask.Domain.Entities;
using TestTask.WebApi.Common;

namespace TestTask.WebApi.Controllers;

public class MoneyAccountsController(
	IMoneyAccountService moneyAccountService) : AuthorizeController
{
	[HttpPost("{currencyId}")]
	public async Task<IActionResult> CreateMoneyAccount(Guid currencyId, CancellationToken cancellationToken)
	{
		var result = await moneyAccountService.CreateAsync(HttpContext.GetAuthenticatedUserId(), new CurrencyId(currencyId), cancellationToken);
		return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
	}

	[HttpGet]
	public async Task<IActionResult> GetAssociatedWithUserMoneyAccounts(CancellationToken cancellationToken)
	{
		var result = await moneyAccountService.GetAllByUserIdAsync(HttpContext.GetAuthenticatedUserId(), cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}
}