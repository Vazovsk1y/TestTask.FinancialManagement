namespace TestTask.Domain.Constants;

public static class DefaultCurrencies
{
	public static readonly (string Title, string NumericCode, string AlphabeticCode) USD = ("United States Dollar", "840", "USD");

	public static readonly (string Title, string NumericCode, string AlphabeticCode) EUR = ("Euro", "978", "EUR");

	public static readonly (string Title, string NumericCode, string AlphabeticCode) GBP = ("British Pound Sterling", "826", "GBP");

	public static readonly (string Title, string NumericCode, string AlphabeticCode) JPY = ("Japanese Yen", "392", "JPY");

	public static readonly (string Title, string NumericCode, string AlphabeticCode) UAH = ("Ukrainian Hryvnia", "980", "UAH");

	public static readonly (string Title, string NumericCode, string AlphabeticCode) RUB = ("Russian Ruble", "643", "RUB");
}