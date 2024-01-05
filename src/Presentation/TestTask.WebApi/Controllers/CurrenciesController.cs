using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services;
using TestTask.WebApi.Common;

namespace TestTask.WebApi.Controllers;

public class CurrenciesController(ICurrencyService currencyService) : BaseController
{
	[HttpGet]
	public async Task<IActionResult> GetAllCurrencies(CancellationToken cancellationToken)
	{
		var result = await currencyService.GetAllAsync(cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}
}