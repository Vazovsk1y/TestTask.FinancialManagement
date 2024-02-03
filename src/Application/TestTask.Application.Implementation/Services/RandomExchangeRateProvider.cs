using TestTask.Application.Common;
using TestTask.Application.Services;

namespace TestTask.Application.Implementation.Services;

public class RandomExchangeRateProvider : IExchangeRateProvider
{
    public Result<decimal> GetRate(string FromAlphabeticCode, string ToAlphabeticCode)
    {
        // TODO
        // Implement receiving rate value from another external api.

        double randomValue = (Random.Shared.NextDouble() * (10.0 - 0.1) + 0.1);
        return (decimal)Math.Round(randomValue, 4);
    }
}