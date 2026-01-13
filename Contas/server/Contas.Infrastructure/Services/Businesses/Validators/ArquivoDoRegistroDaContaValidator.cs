using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos;
using Contas.Core.Objects;
using Contas.Infrastructure.Services.Base;

namespace Contas.Infrastructure.Services.Businesses.Validators;

public class ArquivoDoRegistroDaContaValidator : Validator<ArquivoDoRegistroDaContaDto>, IArquivoDoRegistroDaContaValidator
{
    private ValidationResult validationResult = new();

    public override ValidationResult Validate(ArquivoDoRegistroDaContaDto? dto)
    {        
        validationResult = base.Validate(dto);
        
        if (validationResult.IsValid) SetErrorsConditionally(dto!);

        return validationResult;
    }

    public override ValidationResult Validate(int registroDaContaId)
    {
        if (registroDaContaId <= 0)
            validationResult.AddError("REGISTRO_DA_CONTA_ID_INVALIDO", "O ID do registro da conta deve ser um número positivo.");

        return validationResult;
    }

    private void SetErrorsConditionally(ArquivoDoRegistroDaContaDto dto)
    {        
        validationResult.AddErrorIf(dto.RegistroDaContaId <= 0, "REGISTRO_DA_CONTA_ID_INVALIDO", "O ID do registro da conta deve ser um número positivo.");
        validationResult.AddErrorIf(dto.ArquivoId <= 0, "ARQUIVO_ID_INVALIDO", "O ID do arquivo deve ser um número positivo.");
    }
}
