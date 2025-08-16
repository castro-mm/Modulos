namespace Contas.Api.Objects;

// Pensar em adicionar um campo DataHora para indicar quando a resposta foi gerada
// Pensar em adicionar um campo TraceId para rastreamento dos erros
// Pensar em adicionar um campo Path para indicar o endpoint da API que gerou a resposta
// Pensar em adicionar um campo Errors para detalhar erros específicos, se necessário
// Pensar em um novo modelo de Objeto de Resposta para detalhar melhor as informações
public class ApiResponse(int statusCode, string message, object? data = null, string? details = null)
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public string? Details { get; set; } = details;
    public object? Data { get; set; } = data;
    // TODO: avaliar definir o TraceId para rastreamento dos erros
    // TODO: definir o caminho da api onde o ocorreram os erros
}