using System.Net;
using System.Text.Json;
using Infrastructure.Services;

namespace API.Middlewares;

public class ExceptionMiddleware(IHostEnvironment environment, RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);   
        }
        catch (Exception ex)
        {            
            await HandleExceptionAsync(httpContext, environment, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, IHostEnvironment environment, Exception ex)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = environment.IsDevelopment()
            ? new ValidationResult(){ StatusCode = httpContext.Response.StatusCode, Message = ex.Message, Details = ex.StackTrace }
            : new ValidationResult(){ StatusCode = httpContext.Response.StatusCode, Message = ex.Message, Details = "Internal Server Error" };

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        return httpContext.Response.WriteAsync(json);
    }
}

