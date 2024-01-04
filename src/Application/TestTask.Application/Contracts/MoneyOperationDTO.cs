using System.Text.Json.Serialization;
using TestTask.Domain.Entities;
using TestTask.Domain.Enums;

namespace TestTask.Application.Contracts;

public record MoneyOperationDTO(
	MoneyOperationId Id, 
	MoneyAccountId? MoneyAccountFromId,
	MoneyAccountId? MoneyAccountToId,
	[property: JsonConverter(typeof(JsonStringEnumConverter))]
	MoneyMoveTypes MoneyMoveType,
	[property: JsonConverter(typeof(JsonStringEnumConverter))]
	MoneyOperationTypes OperationType,
	decimal MoneyAmount,
	decimal? CommissionValue,
	DateTimeOffset OperationDate
	);