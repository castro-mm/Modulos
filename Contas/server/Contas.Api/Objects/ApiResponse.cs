namespace Contas.Api.Objects;

// Pensar em um novo modelo de Objeto de Resposta para detalhar melhor as informações
// Ao implementar o IdentityServer, pode ser interessante incluir informações de autenticação e autorização
public class ApiResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public string? Details { get; set; }
    public object? Result { get; set; }
    public DateTime TimeStamp { get; set; }
    public string ApiPath { get; set; }
    public Guid TraceId { get; set; }

    public ApiResponse(int statusCode, string message, DateTime timeStamp, string apiPath, Guid traceId, object? data = null, string? details = null)
    {        
        StatusCode = statusCode;
        Message = message;
        TimeStamp = timeStamp;
        ApiPath = apiPath;
        TraceId = traceId;
        Result = data;
        Details = details;
    }
}