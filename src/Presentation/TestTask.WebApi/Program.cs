using TestTask.DAL;
using TestTask.Application.Implementation;
using TestTask.WebApi;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using FluentValidation;
using TestTask.WebApi.Validators;
using TestTask.BackgroundJobs;
using Hangfire;
using TestTask.WebApi.Filters;
using TestTask.ExchangeRateApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationLayer();
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddExceptionHandler<ExceptionsHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerWithJwt();
builder.Services.AddAuthenticationWithJwtBearer(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddBackgroundJobs(builder.Configuration);
builder.Services.AddExchangeRateProvider(builder.Configuration);

builder.Services.AddValidatorsFromAssembly(typeof(UserCredentialsModelValidator).Assembly);
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseHangfireDashboard(options: new DashboardOptions
{
	Authorization = [ new HangfireDashboardAuthorizationFilter(builder.Environment) ]
});

if (app.Environment.IsDevelopment())
{
	app.MigrateDatabase();
}

app.Run();
