﻿using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services;
using TestTask.Domain.Entities;
using TestTask.WebApi.Common;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Controllers;

[Route("api/money-accounts")]
public class MoneyAccountsController(
	IMoneyAccountService moneyAccountService) : AuthorizeController
{
	[HttpPost]
	public async Task<IActionResult> CreateMoneyAccount(MoneyAccountCreateModel createModel, CancellationToken cancellationToken)
	{
		var result = await moneyAccountService.CreateAsync(HttpContext.GetAuthenticatedUserId(), new CurrencyId(createModel.CurrencyId), cancellationToken);
		return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
	}

	[HttpGet]
	public async Task<IActionResult> GetAssociatedWithUserMoneyAccounts(CancellationToken cancellationToken)
	{
		var result = await moneyAccountService.GetAllByUserIdAsync(HttpContext.GetAuthenticatedUserId(), cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetMoneyAccountById(Guid id, CancellationToken cancellationToken)
	{
		var result = await moneyAccountService.GetByIdAsync(HttpContext.GetAuthenticatedUserId(), new MoneyAccountId(id), cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}
}