using System.Net;

namespace HRCentral.API.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            // Log the Exception
            var errorId = Guid.NewGuid();
            _logger.LogError(ex, $"{errorId}: {ex.Message}");

            // Return a custom error response
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var error = new
            {
                Id = errorId,
                ErrorMessage = "Something went wrong. Please try again later."
            };

            await httpContext.Response.WriteAsJsonAsync(error);
        }
    }
}