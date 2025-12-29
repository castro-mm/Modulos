namespace Contas.Core.Objects;

/// <summary>Classe que representa a estrutura do resultado da validação de uma entidade.</summary>
public class ValidationResult
{
    /// <summary>Lista de erros de validação.</summary>
    public List<ValidationError> Errors { get; set; } = [];  

    /// <summary>Indica se a validação foi bem-sucedida.</summary>
    public bool IsValid => Errors.Count == 0;

    /// <summary>Adiciona um erro de validação à lista.</summary>
    /// <param name="code">Indica o código do erro.</param>
    /// <param name="message">Indica a mensagem do erro.</param>
    public void AddError(string code, string message)
    {
        Errors.Add(new ValidationError(code, message));
    }

    public void AddErrorIf(bool condition, string code, string message)
    {
        if (condition)
            AddError(code, message);
    }
}

/// <summary>Classe que define a estrutura de um erro de validação.</summary>
/// <param name="Code">Indica o código do erro.</param>
/// <param name="Message">Indica a mensagem do erro.</param>
public record ValidationError(string Code, string Message);
