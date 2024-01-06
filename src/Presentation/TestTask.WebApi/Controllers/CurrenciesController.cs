using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services;
using TestTask.Domain.Constants;
using TestTask.WebApi.Common;
using TestTask.WebApi.Validators;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Controllers;

public class CurrenciesController(ICurrencyService currencyService) : BaseController
{
	[HttpGet]
	public async Task<IActionResult> GetAllCurrencies(CancellationToken cancellationToken)
	{
		var result = await currencyService.GetAllAsync(cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}

	[HttpPost]
	[PermittedTo(DefaultRoles.Admin)]
	public async Task<IActionResult> AddCurrency(CurrencyAddModel currencyAddModel, CancellationToken cancellationToken)
	{
		var dto = currencyAddModel.ToDTO();
		var result = await currencyService.AddAsync(dto, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}
}