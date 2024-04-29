using System.Diagnostics;

namespace GameShop.Api.Middleware;

public class RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        Stopwatch stopwatch = new Stopwatch();

        try
        {
            stopwatch.Start();
            await next(context);
        }
        finally
        {
            stopwatch.Stop();

            logger.LogInformation("{RequestMethod} {RequestPath} request took {ElapsedMilliseconds}msec to complete.",
                                   context.Request.Method,
                                   context.Request.Path,
                                   stopwatch.ElapsedMilliseconds);

        }
    }
}
