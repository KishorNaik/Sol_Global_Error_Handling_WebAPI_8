using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Sol_Demo
{
    public sealed class GlobalErrorHandling : IExceptionHandler
    {
        private readonly ILogger<GlobalErrorHandling> logger;

        public GlobalErrorHandling(ILogger<GlobalErrorHandling> logger)
        {
            this.logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = httpContext.Response.StatusCode,
                Title = "Server error",
                Detail = exception.Message
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);

            return true;
        }
    }
}