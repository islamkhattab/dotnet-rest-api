using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Api.ErrorHandling;

public static class ErrorHandlingExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.Run(async (context) =>
        {
            var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("Error Handler");

            var excpetionDetails = context.Features.Get<IExceptionHandlerFeature>();
            var ex = excpetionDetails?.Error;

            logger.LogError(ex, "Couldn't process the request on machine {Machine} - Trace Id {Trace Id}",
                                Environment.MachineName,
                                Activity.Current?.TraceId);

            var problem = new ProblemDetails
            {
                Title = "Error processing the request",
                Status = StatusCodes.Status500InternalServerError,
                Extensions =
                {
                    { "traceId", Activity.Current?.TraceId.ToString() },
                }
            };

            if (context.RequestServices.GetRequiredService<IHostEnvironment>().IsDevelopment())
            {
                problem.Detail = ex?.ToString();
            }

            await Results.Problem(problem).ExecuteAsync(context);
        });
    }
}