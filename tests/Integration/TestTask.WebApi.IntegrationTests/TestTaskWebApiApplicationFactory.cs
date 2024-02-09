using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using TestTask.DAL;
using TestTask.Domain.Constants;
using TestTask.Domain.Entities;
using TestTask.WebApi.ViewModels;

namespace TestTask.WebApi.IntegrationTests;

public class TestTaskWebApiApplicationFactory : 
    WebApplicationFactory<Program>, 
    IAsyncLifetime
{
    private readonly MsSqlContainer _container = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPassword("Mike_Vazovsk1y_pizdatiy_developer")
        .WithCleanUp(true)
        .Build();

    private static readonly IReadOnlyCollection<Role> __roles = new string[]
    {
        DefaultRoles.User,
        DefaultRoles.Admin
    }
    .Select(e => new Role
    {
        Title = e
    })
    .ToList();

    public readonly UserRegisterModel Admin = new
    (
        "Mike Vazovsk1y",
        new UserCredentialsModel("testEmail@gmail.com", "strongPassword228#")
    );

    public readonly UserRegisterModel User = new
    (
        "Simple User",
        new UserCredentialsModel("simpleUser@gmail.com", "notStrongPassword228#")
    );

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((host, _) =>
        {
            host.HostingEnvironment.EnvironmentName = "Staging";
        });

        builder.ConfigureTestServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<TestTaskDbContext>));

            if (dbContextDescriptor is not null)
            {
                services.Remove(dbContextDescriptor);
            }

            services.AddDbContext<TestTaskDbContext>(options =>
                options.UseSqlServer(_container.GetConnectionString()));
        });
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        await MigrateDatabase();
        await SeedDatabase();
    }
    public new async Task DisposeAsync()
    {
        await _container.StopAsync();
    }

    private async Task MigrateDatabase()
    {
        using var scope = Server.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    private async Task SeedDatabase()
    {
        using var scope = Server.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TestTaskDbContext>();

        var adminRole = __roles.Single(e => e.Title == DefaultRoles.Admin);
        var userRole = __roles.Single(e => e.Title == DefaultRoles.User);

        var admin = new User
        {
            Email = Admin.Credentials.Email,
            FullName = Admin.FullName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(Admin.Credentials.Password),
        };

        var simpleUser = new User
        {
            Email = User.Credentials.Email,
            FullName = User.FullName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(User.Credentials.Password),
        };

        var usersRoles = new List<UserRole>()
        {
            new() { RoleId = adminRole.Id, UserId = admin.Id },
            new() { RoleId = userRole.Id, UserId = admin.Id },
            new() { RoleId = userRole.Id, UserId = simpleUser.Id }
        };

        dbContext.Users.AddRange(admin, simpleUser);
        dbContext.UserRoles.AddRange(usersRoles);
        dbContext.Roles.AddRange(__roles);

        await dbContext.SaveChangesAsync();
    }
}
