namespace TestTask.WebApi.ViewModels;

public record TransferModel(Guid MoneyAccountFromId, Guid MoneyAccountToId, decimal MoneyAmount);