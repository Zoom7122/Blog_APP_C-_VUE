namespace BlagAPP_MVC.Middleware;

public class UnhandledExceptionLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UnhandledExceptionLoggingMiddleware> _logger;

    public UnhandledExceptionLoggingMiddleware(
        RequestDelegate next,
        ILogger<UnhandledExceptionLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Unhandled exception. Method: {Method}, Path: {Path}",
                context.Request.Method,
                context.Request.Path);
            throw;
        }
    }
}
