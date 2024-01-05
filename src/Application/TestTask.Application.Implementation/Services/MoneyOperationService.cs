using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Application.Services;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

internal class MoneyOperationService : IMoneyOperationService
{
	public Task<Result<MoneyOperationId>> EnrollAsync(EnrollDTO enrollDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result<IReadOnlyCollection<MoneyOperationDTO>>> GetAllByMoneyAccountIdAsync(UserId requesterId, MoneyAccountId moneyAccountId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result<IReadOnlyCollection<MoneyOperationDTO>>> GetAllByUserIdAsync(UserId requesterId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result<MoneyOperationId>> TransferAsync(UserId requesterId, TransferDTO transferDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Result<MoneyOperationId>> WithdrawalAsync(UserId requesterId, WithdrawalDTO withdrawalDTO, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
