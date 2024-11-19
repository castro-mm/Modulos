using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services;

public class ValidationResult : IValidationResult
{   
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public string? Details { get; set; }
    public List<object> Data { get; set; } = [];

    public ValidationResult Add(int statusCode, string message = "", string details = "", object? data = null)
    {
        StatusCode = statusCode;
        Message = message ?? Message;
        Details = details ?? Details;
        if (data != null) Data.Add(data);
        
        return this;
    }
}