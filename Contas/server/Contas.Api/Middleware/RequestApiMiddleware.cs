using System.Text.Json;
using Contas.Api.Objects;
using Contas.Core.Dtos.System;
using Contas.Infrastructure.Services.Interfaces.System;

namespace Contas.Api.Middleware;

// TODO: Criar uma tabela de Log de Erros no banco de dados para registrar as exceções.
public class RequestApiMiddleware
{
    private readonly Guid traceId = Guid.NewGuid();
    private readonly IHostEnvironment _env;
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestApiMiddleware> _logger;
    private readonly IServiceProvider _serviceProvider;

    public RequestApiMiddleware(IHostEnvironment env, RequestDelegate next, ILogger<RequestApiMiddleware> logger, IServiceProvider serviceProvider)
    {
        this._env = env;
        this._next = next;
        this._logger = logger;
        this._serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        // Cria um MemoryStream para capturar a resposta
        var originalBodyStream = httpContext.Response.Body;
        using var memoryStream = new MemoryStream();
        httpContext.Response.Body = memoryStream;

        try
        {
            // Executa a próxima etapa do pipeline
            await _next(httpContext);

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
            details: _env.IsDevelopment()
                ? (exception.InnerException == null ? (exception.StackTrace ?? "Detalhamento não disponível") : exception.InnerException.Message)
                : "Internal Server Error"
        );

        _logger.LogError(exception, "Exception on path: {Path}.", httpContext.Request.Path);

        var logDeErro = new LogDeErroDto
        {
            Mensagem = exception.Message,
            Detalhes = exception.InnerException == null ? (exception.StackTrace ?? "Detalhamento não disponível") : exception.InnerException.Message,
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
            using var scope = _serviceProvider.CreateScope();
            var logDeErroService = scope.ServiceProvider.GetRequiredService<ILogDeErroService>();

            var result = await logDeErroService.CreateAsync(logDeErro, default);

            _logger.LogInformation("Error logged successfully.");
        }
        catch (Exception logEx)
        {
            _logger.LogError(logEx, "Failed to log error to the database.");
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

        _logger.LogInformation("Request cancelled by the user on path: {Path}.", httpContext.Request.Path);
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

        _logger.LogInformation("Response sent with status code {StatusCode} for path: {Path}.", httpContext.Response.StatusCode, httpContext.Request.Path);
    }
}