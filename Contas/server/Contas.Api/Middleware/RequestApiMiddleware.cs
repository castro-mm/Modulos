using System.Text.Json;
using Contas.Api.Objects;
using Contas.Core.Dtos.System;
using Contas.Infrastructure.Services.Interfaces.System;

namespace Contas.Api.Middleware;

// TODO: Criar uma tabela de Log de Erros no banco de dados para registrar as exceções.
public class RequestApiMiddleware(
    IHostEnvironment env,
    RequestDelegate next,
    ILogger<RequestApiMiddleware> logger,
    IServiceProvider serviceProvider
)
{
    private readonly Guid traceId = Guid.NewGuid();

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

            memoryStream.Seek(0, SeekOrigin.Begin);
            var originalResponse = await new StreamReader(memoryStream).ReadToEndAsync();

            await HandleResponseBodyAsync(
                httpContext,
                originalBodyStream,
                httpContext.Response.ContentType?.Contains("text/plain") == true
                    ? originalResponse
                    : default!,
                data: httpContext.Response.ContentType?.Contains("application/json") == true
                    ? JsonSerializer.Deserialize<object>(originalResponse)
                    : null
            );
        }
        catch (OperationCanceledException) when (httpContext.RequestAborted.IsCancellationRequested)
        {
            await HandleOperationCancelledAsync(httpContext, originalBodyStream);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, originalBodyStream);
        }
        finally
        {
            httpContext.Response.Body = originalBodyStream;
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, Stream originalBodyStream)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await HandleResponseBodyAsync(
            httpContext,
            originalBodyStream,
            exception.Message,
            details: env.IsDevelopment() ? exception.StackTrace : "Internal Server Error"
        );

        logger.LogError(exception, "Exception on path: {Path}.", httpContext.Request.Path);

        // Log the error to the database
        var logDeErro = new LogDeErroDto
        {
            Mensagem = exception.Message,
            Detalhes = exception.StackTrace ?? "Detalhamento não disponível",
            Metodo = httpContext.Request.Method,
            Caminho = httpContext.Request.Path,
            Ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "IP não disponível",
            Navegador = httpContext.Request.Headers.UserAgent.ToString(),
            DataDeCriacao = DateTime.Now,
            DataDeAtualizacao = DateTime.Now,
            Usuario = httpContext.User.Identity?.Name ?? "Anonymous",
            TraceId = traceId
        };

        try
        {
            using var scope = serviceProvider.CreateScope();
            var logDeErroService = scope.ServiceProvider.GetRequiredService<ILogDeErroService>();

            var result = await logDeErroService.CreateAsync(logDeErro, default);

            logger.LogInformation("Error logged successfully.");
        }
        catch (Exception logEx)
        {
            logger.LogError(logEx, "Failed to log error to the database.");
        }
    }

    private async Task HandleOperationCancelledAsync(HttpContext httpContext, Stream originalBodyStream)
    {
        httpContext.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;

        await HandleResponseBodyAsync(
            httpContext,
            originalBodyStream,
            "A requisição foi cancelada pelo cliente.",
            details: "Client Closed Request"
        );

        logger.LogInformation("Request cancelled by the user on path: {Path}.", httpContext.Request.Path);
    }

    private async Task HandleResponseBodyAsync(HttpContext httpContext, Stream originalBodyStream, string message, object? data = null, string? details = null)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.Body = originalBodyStream;

        var response = new ApiResponse(
            httpContext.Response.StatusCode,
            message,
            DateTime.Now,
            httpContext.Request.Path,
            traceId,
            data,
            details
        );

        await httpContext.Response.WriteAsJsonAsync(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        logger.LogInformation("Response sent with status code {StatusCode} for path: {Path}.", httpContext.Response.StatusCode, httpContext.Request.Path);
    }
}