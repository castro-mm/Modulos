namespace Infrastructure.Services.Base;

public abstract class Service : IService
{
    public ValidationResult ValidationResult { get; set; } = new();
}
