using Contas.Core.Dtos.System;
using Contas.Core.Objects;

namespace Contas.Core.Businesses.Validators.Interfaces;

public interface IArquivoValidator : IValidator<ArquivoDto>
{
    ValidationResult Validate(int id);
}
