using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Domain.Entities;

namespace TestTask.Application.Services;

public interface ICommissionService
{
	Task<Result<CommissionId>> CreateAsync(UserId requesterId, CommissionCreateDTO commissionCreateDTO, CancellationToken cancellationToken = default);
}