using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }       

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch(Exception ex)
        {
            if (ex.InnerException != null)
            {
                _logger.LogError("{ExceptionType}{ExceptionMessage}",
                ex.InnerException.GetType().ToString(),
                ex.InnerException.Message);
            }
            else
            {
                _logger.LogError("{ExceptionType}{ExceptionMessage}",
                ex.GetType().ToString(), ex.Message);
            }

            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(new { Message = ex.Message, Type = ex.GetType().ToString() });
        }
    }
}

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}