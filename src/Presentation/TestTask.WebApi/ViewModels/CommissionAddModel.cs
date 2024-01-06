namespace TestTask.WebApi.ViewModels;

public record CommissionAddModel(Guid CurrencyFromId, Guid CurrencyToId, decimal Value);