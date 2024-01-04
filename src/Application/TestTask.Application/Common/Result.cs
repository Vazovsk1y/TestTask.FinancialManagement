namespace TestTask.Application.Common;

public record Result
{
	public string ErrorMessage { get; }

	public bool IsSuccess { get; }

	public bool IsFailure => !IsSuccess;

	protected Result(bool isSuccess, string? errorMessage)
	{
		IsSuccess = isSuccess;
		ErrorMessage = errorMessage ?? string.Empty;
	}

	public static Result Success() => new(true, null);

	public static Result<T> Success<T>(T value) => new(value, true, null);

	public static Result Failure(string errorMessage) => new(false, errorMessage);

	public static Result<T> Failure<T>(string errorMessage) => new(default, false, errorMessage);
}

public record Result<T> : Result
{
	private readonly T? _value;

	protected internal Result(T? value, bool isSuccess, string? errorMessage) : base(isSuccess, errorMessage)
		=> _value = value;

	public T Value => IsFailure ?
		throw new InvalidOperationException("The value of failed result can't be accessed.")
		:
		_value!;

	public static implicit operator Result<T>(T value) => new(value, true, null);
}
