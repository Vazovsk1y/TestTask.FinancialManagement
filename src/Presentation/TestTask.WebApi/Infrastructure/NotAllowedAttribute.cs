using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestTask.WebApi.Infrastructure;

public class NotAllowedAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        context.Result = new ContentResult
        {
            StatusCode = 405,
            Content = "This endpoint is not yet supported."
        };
    }
}
