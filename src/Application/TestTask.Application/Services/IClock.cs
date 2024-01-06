namespace TestTask.Application.Services;

public interface IClock
{
	DateTimeOffset GetUtcNow();
}