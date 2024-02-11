using System.Text.RegularExpressions;

namespace TestTask.Domain.Constants;

public static partial class Constraints
{
	public static class Commission
	{
		public const decimal MaxCommissionValue = 0.3m;

		public static readonly (int Precision, int Scale) ValuePrecision = (4, 2);
	}

    public static class ExchangeRate
    {
        public static readonly (int Precision, int Scale) ValuePrecision = (19, 4);
    }

    public static class Currency
	{
		public const int MaxTitleLength = 75;

        public static bool IsSupported(string title, string alphabeticCode, string numericCode)
        {
            return Currencies.Supported.Any(e => e == (title, alphabeticCode, numericCode));
        }
    }

	public static class CurrencyCodes
	{
		public const int CodeMaxLength = 3;

		public static bool IsNumericCode(string numericCode)
		{
			if (string.IsNullOrWhiteSpace(numericCode))
			{
				return false;
			}

			return numericCode.Length <= CodeMaxLength && numericCode.All(char.IsDigit);
		}

		public static bool IsAlphabeticCode(string alphabeticCode)
		{
			if (string.IsNullOrWhiteSpace(alphabeticCode))
			{
				return false;
			}

			foreach (char c in alphabeticCode)
			{
				if (!IsLatinLetter(c) || !char.IsUpper(c))
				{
					return false;
				}
			}

			return true;
		}

		private static bool IsLatinLetter(char letter)
		{
			return (letter >= 'A' && letter <= 'Z') || (letter >= 'a' && letter <= 'z');
		}
	}

    public static class Role
    {
		public const int MaxTitleLength = 15;
    }

	public static partial class User
	{
		public const int MaxFullNameLength = 50;

		public const int MaxEmailLength = 50;
	}

	public static partial class Credentials
	{
		[GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
		public static partial Regex PasswordRegex();
	}

	public static class MoneyAccount
	{
		public static readonly (int Precision, int Scale) BalancePrecision = (19, 4);
	}

	public static class MoneyOperation
	{
		public static readonly (int Precision, int Scale) MoneyAmountPrecision = (19, 4);
	}
}