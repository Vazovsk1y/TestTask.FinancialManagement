using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using TestTask.DAL.SQLServer;
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

    private static readonly IReadOnlyCollection<Role> Roles = new string[]
    {
        DefaultRoles.User,
        DefaultRoles.Admin
    }
    .Select(e => new Role
    {
        Title = e
    })
    .ToList();

    public readonly UserRegisterModel AdminRegisterModel = new
    (
        "Mike Vazovsk1y",
        new UserCredentialsModel("testEmail@gmail.com", "strongPassword228#")
    );

    public readonly UserRegisterModel UserRegisterModel = new
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

        var adminRole = Roles.Single(e => e.Title == DefaultRoles.Admin);
        var userRole = Roles.Single(e => e.Title == DefaultRoles.User);

        var admin = new User
        {
            Email = AdminRegisterModel.Credentials.Email,
            FullName = AdminRegisterModel.FullName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(AdminRegisterModel.Credentials.Password),
        };

        var simpleUser = new User
        {
            Email = UserRegisterModel.Credentials.Email,
            FullName = UserRegisterModel.FullName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(UserRegisterModel.Credentials.Password),
        };

        var usersRoles = new List<UserRole>()
        {
            new() { RoleId = adminRole.Id, UserId = admin.Id },
            new() { RoleId = userRole.Id, UserId = admin.Id },
            new() { RoleId = userRole.Id, UserId = simpleUser.Id }
        };

        dbContext.Users.AddRange(admin, simpleUser);
        dbContext.UserRoles.AddRange(usersRoles);
        dbContext.Roles.AddRange(Roles);

        await dbContext.SaveChangesAsync();
    }
}
