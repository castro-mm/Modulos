namespace Infrastructure.Services.Interfaces;

public interface IValidationResult
{
    ValidationResult Add(int statusCode, string message = "", string details = "", object? data = null);
}
