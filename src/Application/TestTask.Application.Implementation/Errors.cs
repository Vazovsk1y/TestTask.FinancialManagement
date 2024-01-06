namespace TestTask.Application.Implementation;

public static class Errors
{
	public static string EntityWithPassedIdIsNotExists(string entityName) => $"{entityName} with passed id is not exists.";

	public static string EntityWithPropertyIsAlreadyExists(string entityName, string propertyName) => $"{entityName} with passed {propertyName} is already exists.";

	public const string EmailIsAlreadyTaken = "Email is already taken.";

	public static class Auth
	{
		public const string InvalidCredentials = "Email or password was incorrect.";

		public const string AccessDenied = "Access to this action denied.";

		public const string Unauthenticated = "User was unauthenticated.";
	}

	public static class MoneyAccount
	{
		public const string AccountWithPassedCurrencyIsAlreadyCreated = "User already have an account with passed currency.";
	}
}