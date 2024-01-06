using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services;
using TestTask.Domain.Constants;
using TestTask.WebApi.Common;
using TestTask.WebApi.Validators;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.Controllers;

public class CommissionsController(ICommissionService commissionService) : AuthorizeController
{
	[PermittedTo(DefaultRoles.Admin)]
	[HttpPost]
	public async Task<IActionResult> AddCommission(CommissionAddModel commissionAddModel, CancellationToken cancellationToken)
	{
		var dto = commissionAddModel.ToDTO();
		var result = await commissionService.AddAsync(dto, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}
}