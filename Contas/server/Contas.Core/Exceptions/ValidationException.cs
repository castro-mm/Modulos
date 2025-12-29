using Contas.Core.Objects;

namespace Contas.Core.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationError> errors)
        : base("Ocorreram erros de validação")
    {
        Errors = errors;
    }

    public IEnumerable<ValidationError> Errors { get; }

    public override string ToString()
    {
        return $"{base.ToString()}, Errors: {string.Join(", ", Errors.Select(e => e.Message))}";
    }
}
