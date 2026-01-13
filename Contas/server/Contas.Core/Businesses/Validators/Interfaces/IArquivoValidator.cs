using Contas.Core.Dtos.System;
using Contas.Core.Objects;
using Microsoft.AspNetCore.Http;

namespace Contas.Core.Businesses.Validators.Interfaces;

public interface IArquivoValidator : IValidator<ArquivoDto>
{
    ValidationResult Validate(int id);
    ValidationResult Validate(IFormFile file);
}
