using System.Text.Json;
using Contas.Api.Objects;

namespace Contas.Api.Middleware;

public class ExceptionApiMiddleware(IHostEnvironment env, RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        // Cria um MemoryStream para capturar a resposta
        var originalBodyStream = httpContext.Response.Body;
        using var memoryStream = new MemoryStream();
        httpContext.Response.Body = memoryStream;

        try
        {
            // Executa a próxima etapa do pipeline
            await next(httpContext);

            // Formata a resposta apenas se o status for 200 (OK)
            if (httpContext.Response.StatusCode == StatusCodes.Status200OK && httpContext.Response.ContentType?.Contains("application/json") == true)
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                var originalResponse = await new StreamReader(memoryStream).ReadToEndAsync();

                var response = new ApiResponse(
                    httpContext.Response.StatusCode,
                    httpContext.Response.StatusCode == StatusCodes.Status200OK ? "Success" : "Error",
                    data: JsonSerializer.Deserialize<object>(originalResponse)
                );

                httpContext.Response.Body = originalBodyStream; // Restaura o stream original

                await httpContext.Response.WriteAsJsonAsync(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });
            }
            else
            {
                // Restaura o stream original e copia o conteúdo
                memoryStream.Seek(0, SeekOrigin.Begin);
                await memoryStream.CopyToAsync(originalBodyStream);
            }
        }
        catch (OperationCanceledException) when (httpContext.RequestAborted.IsCancellationRequested)
        {
            // Trata exceções e retorna uma resposta padronizada
            await HandleOperationCancelledAsync(httpContext, originalBodyStream);
        }
        catch (Exception ex)
        {
            // Trata exceções e retorna uma resposta padronizada
            await HandleExceptionAsync(httpContext, ex, originalBodyStream);
        }
        finally
        {
            // Garante que o stream original seja restaurado
            httpContext.Response.Body = originalBodyStream;
        }
    }

    private Task HandleExceptionAsync(HttpContext httpContext, Exception exception, Stream originalBodyStream)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.Body = originalBodyStream;
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new ApiResponse(
            httpContext.Response.StatusCode,
            exception.Message,
            details: env.IsDevelopment() ? exception.StackTrace : "Internal Server Error");

        // TODO: Log the exception (e.g., using a logging framework)
        // TODO: Consider using a logging framework like Serilog or NLog to log the exception details.
        // TODO: Criar uma tabela de Log de Erros no banco de dados para registrar as exceções.

        return httpContext.Response.WriteAsJsonAsync(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
    }
    
    private static Task HandleOperationCancelledAsync(HttpContext httpContext, Stream originalBodyStream)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.Body = originalBodyStream;
        httpContext.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;

        var response = new ApiResponse(
            httpContext.Response.StatusCode,
            "Operação cancelada pelo cliente.",
            details: "Client Closed Request");

        return httpContext.Response.WriteAsJsonAsync(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
    }
    
}