using TestTask.Application.Contracts;
using TestTask.Application.Shared;
using TestTask.Domain.Entities;

namespace TestTask.Application.Services;

public interface ICommissionService
{
	Task<Result<CommissionId>> AddAsync(CommissionAddDTO commissionAddDTO, CancellationToken cancellationToken = default);

	Task<Result<IReadOnlyCollection<CommissionDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
}