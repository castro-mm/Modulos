using Contas.Core.Dtos;
using Contas.Core.Objects;

namespace Contas.Core.Businesses.Validators.Interfaces;

public interface IArquivoDoRegistroDaContaValidator : IValidator<ArquivoDoRegistroDaContaDto>
{
    ValidationResult Validate(int registroDaContaId);    
}
