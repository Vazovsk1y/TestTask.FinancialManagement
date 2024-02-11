using TestTask.Application.Common;
using TestTask.Application.Contracts;
using TestTask.Domain.Entities;

namespace TestTask.Application.Services;

public interface IMoneyOperationService
{
	Task<Result<MoneyOperationId>> EnrollAsync(EnrollDTO enrollDTO, CancellationToken cancellationToken = default);

	Task<Result<MoneyOperationId>> WithdrawalAsync(WithdrawalDTO withdrawalDTO, CancellationToken cancellationToken = default);

	Task<Result<TransferResponseDTO>> TransferAsync(TransferDTO transferDTO, CancellationToken cancellationToken = default);

	Task<Result<IReadOnlyCollection<MoneyOperationDTO>>> GetAllByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);

	Task<Result<IReadOnlyCollection<MoneyOperationDTO>>> GetAllByMoneyAccountIdAsync(MoneyAccountId moneyAccountId, CancellationToken cancellationToken = default);
}