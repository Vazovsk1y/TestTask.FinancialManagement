using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using TestTask.DAL;

namespace TestTask.WebApi.IntegrationTests;

public abstract class IntegrationTest : 
    IClassFixture<TestTaskWebApiApplicationFactory>, 
    IDisposable
{
    private readonly TestTaskWebApiApplicationFactory _factory;
    private readonly IServiceScope _scope;
    protected readonly HttpClient _httpClient;
    protected readonly TestTaskDbContext _dbContext;
    protected IntegrationTest(TestTaskWebApiApplicationFactory factory)
    {
        _factory = factory;
        _scope = _factory.Services.CreateScope();
        _httpClient = _factory.CreateClient();
        _dbContext = _scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();
    }

    protected async Task<string> LoginAsUserAsync()
    {
        var response = await _httpClient.PostAsJsonAsync("api/users/sign-in", _factory.User.Credentials);
        return await response.Content.ReadAsStringAsync();
    }

    protected async Task<string> LoginAsAdminAsync()
    {
        var response = await _httpClient.PostAsJsonAsync("api/users/sign-in", _factory.Admin.Credentials);
        return await response.Content.ReadAsStringAsync();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        _scope.Dispose();
    }
}
