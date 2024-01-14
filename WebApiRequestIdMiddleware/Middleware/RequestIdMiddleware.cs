namespace WebApiRequestIdMiddleware.Middleware;

public class RequestIdMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ILogger<RequestIdMiddleware> logger)
    {
        var requestId = context.Request.Headers["X-Request-ID"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(requestId))
        {
            requestId = Guid.NewGuid().ToString();
            context.Request.Headers["X-Request-ID"] = requestId;
        }

        using (
            logger.BeginScope(
                new ConsoleLoggerDictionary<string, object> { ["X-Request-ID"] = requestId }
            )
        )
        {
            await next(context);
        }
    }
}
