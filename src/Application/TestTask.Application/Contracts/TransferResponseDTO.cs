using TestTask.Domain.Entities;

namespace TestTask.Application.Contracts;

public record TransferResponseDTO(MoneyOperationId Sub, MoneyOperationId Add);