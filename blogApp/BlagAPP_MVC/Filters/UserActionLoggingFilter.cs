using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace BlagAPP_MVC.Filters;

public class UserActionLoggingFilter : IAsyncActionFilter
{
    private readonly ILogger<UserActionLoggingFilter> _logger;

    public UserActionLoggingFilter(ILogger<UserActionLoggingFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;
        var userEmail = httpContext.User.FindFirstValue(ClaimTypes.Email) ?? "anonymous";

        _logger.LogInformation(
            "User action started. Method: {Method}, Path: {Path}, Controller: {Controller}, Action: {Action}, User: {User}",
            httpContext.Request.Method,
            httpContext.Request.Path,
            context.RouteData.Values["controller"],
            context.RouteData.Values["action"],
            userEmail);

        var executedContext = await next();

        _logger.LogInformation(
            "User action completed. Method: {Method}, Path: {Path}, StatusCode: {StatusCode}, User: {User}",
            httpContext.Request.Method,
            httpContext.Request.Path,
            httpContext.Response.StatusCode,
            userEmail);

        if (executedContext.Exception is not null && !executedContext.ExceptionHandled)
        {
            _logger.LogError(executedContext.Exception,
                "Unhandled exception in MVC action. Controller: {Controller}, Action: {Action}, User: {User}",
                context.RouteData.Values["controller"],
                context.RouteData.Values["action"],
                userEmail);
        }
    }
}
