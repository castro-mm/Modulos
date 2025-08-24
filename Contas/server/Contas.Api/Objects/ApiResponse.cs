namespace Contas.Api.Objects;

// Pensar em um novo modelo de Objeto de Resposta para detalhar melhor as informações
// Ao implementar o IdentityServer, pode ser interessante incluir informações de autenticação e autorização
public class ApiResponse(int statusCode, string message, DateTime timeStamp, string apiPath, Guid traceId, object? data = null, string? details = null)
{
    public int StatusCode { get; set; } = statusCode;
    public string? Message { get; set; } = message;
    public string? Details { get; set; } = details;
    public object? Data { get; set; } = data;
    public DateTime TimeStamp { get; set; } = timeStamp;
    public string ApiPath { get; set; } = apiPath;
    public Guid TraceId { get; set; } = traceId;
}