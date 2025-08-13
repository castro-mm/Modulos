namespace Contas.Api.Objects;

public class ApiResponse(int statusCode, string message, object? data = null, string? details = null)
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public string? Details { get; set; } = details;
    public object? Data { get; set; } = data;
    // TODO: avaliar definir o TraceId para rastreamento dos erros
    // TODO: definir o caminho da api onde o ocorreram os erros
}