using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using TestTask.DAL.SQLServer;
using TestTask.Domain.Entities;

namespace TestTask.WebApi.IntegrationTests;

public abstract class IntegrationTest : 
    IClassFixture<TestTaskWebApiApplicationFactory>,
    IAsyncLifetime
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

    protected User User { get; private set; } = default!;

    protected User Admin { get; private set; } = default!;

    protected async Task<string> LoginAsUserAsync()
    {
        var response = await _httpClient.PostAsJsonAsync("api/users/sign-in", _factory.UserRegisterModel.Credentials);
        return await response.Content.ReadAsStringAsync();
    }

    protected async Task<string> LoginAsAdminAsync()
    {
        var response = await _httpClient.PostAsJsonAsync("api/users/sign-in", _factory.AdminRegisterModel.Credentials);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task InitializeAsync()
    {
        User = await _dbContext
            .Users
            .Include(e => e.Roles)
            .ThenInclude(e => e.Role)
            .SingleAsync(e => e.Email == _factory.UserRegisterModel.Credentials.Email);

        Admin = await _dbContext
            .Users
            .Include(e => e.Roles)
            .ThenInclude(e => e.Role)
            .SingleAsync(e => e.Email == _factory.AdminRegisterModel.Credentials.Email);
    }

    public Task DisposeAsync()
    {
        _httpClient.Dispose();
        _scope.Dispose();
        return Task.CompletedTask;
    }
}
