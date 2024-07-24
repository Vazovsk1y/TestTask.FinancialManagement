using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services;
using TestTask.Domain.Entities;
using TestTask.WebApi.Controllers.Common;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Controllers;

[Route("api/money-accounts")]
public class MoneyAccountsController(
	IMoneyAccountService moneyAccountService,
	IMoneyOperationService moneyOperationService) : AuthorizeController
{
	[HttpPost]
	public async Task<IActionResult> CreateMoneyAccount(MoneyAccountCreateModel createModel, CancellationToken cancellationToken)
	{
		var result = await moneyAccountService.CreateAsync(new CurrencyId(createModel.CurrencyId), cancellationToken);
		return result.IsSuccess ? Created(string.Empty, result.Value) : BadRequest(result.ErrorMessage);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetMoneyAccountById(Guid id, CancellationToken cancellationToken)
	{
		var result = await moneyAccountService.GetByIdAsync(new MoneyAccountId(id), cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}

	[HttpGet("{id}/money-operations")]
	public async Task<IActionResult> GetAllMoneyOperationsByMoneyAccountId(Guid id, CancellationToken cancellationToken)
	{
		var result = await moneyOperationService.GetAllByMoneyAccountIdAsync(new MoneyAccountId(id), cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}
}