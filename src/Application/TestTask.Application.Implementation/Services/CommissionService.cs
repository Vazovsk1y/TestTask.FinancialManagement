using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Application.Services;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

internal class CommissionService : ICommissionService
{
	public Task<Result<CommissionId>> CreateAsync(UserId requesterId, CommissionCreateDTO commissionCreateDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}