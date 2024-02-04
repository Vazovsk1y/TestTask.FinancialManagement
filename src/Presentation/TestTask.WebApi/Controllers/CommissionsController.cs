using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Services;
using TestTask.Domain.Constants;
using TestTask.WebApi.Common;
using TestTask.WebApi.Filters;
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

	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> GetAllCommissions(CancellationToken cancellationToken)
	{
		var result = await commissionService.GetAllAsync(cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
	}

	[HttpDelete("{id}")]
	[PermittedTo(DefaultRoles.Admin)]
	[NotAllowed]
	public Task<IActionResult> DeleteCommissionById(Guid id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}