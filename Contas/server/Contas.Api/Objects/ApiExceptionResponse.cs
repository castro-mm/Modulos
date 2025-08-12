namespace Contas.Api.Objects;

public class ApiExceptionResponse(int statusCode, string message, string? details = null) : ApiResponse(statusCode, message)
{
    public string? Details { get; set; } = details;
}