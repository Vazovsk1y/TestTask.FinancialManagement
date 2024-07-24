using Hangfire.Annotations;
using Hangfire.Dashboard;
using TestTask.Domain.Constants;

namespace TestTask.WebApi.Infrastructure;

internal class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    private readonly IHostEnvironment _environment;
    internal HangfireDashboardAuthorizationFilter(IHostEnvironment environment)
    {
        _environment = environment;
    }
    public bool Authorize([NotNull] DashboardContext context)
    {
        if (!_environment.IsProduction())
        {
            return true;
        }

        var httpContext = context.GetHttpContext();
        return httpContext.User.Identity?.IsAuthenticated is true && httpContext.User.IsInRole(DefaultRoles.Admin);
    }
}
