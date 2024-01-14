using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiRequestIdMiddleware.Filters;

public class ExceptionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null)
        {
            return;
        }
        context.Result = new ObjectResult(
            new
            {
                RequestId = context.HttpContext.Request.Headers["X-Request-ID"].FirstOrDefault(),
                context.Exception.Message
            }
        )
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}
