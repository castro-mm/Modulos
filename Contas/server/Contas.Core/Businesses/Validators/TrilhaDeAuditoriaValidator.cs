using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos.System;
using Contas.Core.Objects;

namespace Contas.Core.Businesses.Validators;

public class TrilhaDeAuditoriaValidator : Validator<TrilhaDeAuditoriaDto>, ITrilhaDeAuditoriaValidator
{
    private ValidationResult validationResult = new();

    public override ValidationResult Validate(TrilhaDeAuditoriaDto? dto)
    {
        validationResult = base.Validate(dto);
        
        if (validationResult.IsValid) SetErrorsConditionally(dto!);

        return validationResult;
    }

    private static void SetErrorsConditionally(TrilhaDeAuditoriaDto dto)
    {
        // Acrescente validações específicas para TrilhaDeAuditoriaDto, se necessário        
        // ...
    }
}
