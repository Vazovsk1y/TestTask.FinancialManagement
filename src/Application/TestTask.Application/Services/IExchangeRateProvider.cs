using TestTask.Application.Common;

namespace TestTask.Application.Services;

public interface IExchangeRateProvider
{
	Result<decimal> GetRate(string FromAlphabeticCode, string ToAlphabeticCode);
}