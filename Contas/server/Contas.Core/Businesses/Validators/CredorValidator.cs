using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos;
using Contas.Core.Objects;

namespace Contas.Core.Businesses.Validators;

public class CredorValidator : Validator<CredorDto>, ICredorValidator
{
    private ValidationResult validationResult = new();
    public override ValidationResult Validate(CredorDto? dto)
    {
        validationResult = base.Validate(dto);
        
        if (validationResult.IsValid) SetErrorsConditionally(dto!);
        
        return validationResult;
    }

    private void SetErrorsConditionally(CredorDto dto)
    {
        validationResult.AddErrorIf(dto.SegmentoDoCredorId == 0, "SEGMENTO_CREDOR_OBRIGATORIO", "O segmento do credor é obrigatório.");
        validationResult.AddErrorIf(string.IsNullOrWhiteSpace(dto.NomeFantasia), "NOME_OBRIGATORIO", "O Nome Fantasia do credor é obrigatório.");
        validationResult.AddErrorIf(dto.NomeFantasia.Length < 3, "NOME_INVALIDO", "O Nome Fantasia do credor deve ter pelo menos 03 caracteres.");
        validationResult.AddErrorIf(dto.NomeFantasia.Length > 100, "NOME_EXCEDENTE", "O Nome Fantasia do credor não pode exceder 100 caracteres.");
        validationResult.AddErrorIf(string.IsNullOrWhiteSpace(dto.RazaoSocial), "RAZAO_SOCIAL_OBRIGATORIO", "A Razão Social do credor é obrigatória.");
        validationResult.AddErrorIf(dto.RazaoSocial.Length < 3, "RAZAO_SOCIAL_INVALIDA", "A Razão Social do credor deve ter pelo menos 03 caracteres.");
        validationResult.AddErrorIf(dto.RazaoSocial.Length > 150, "RAZAO_SOCIAL_EXCEDENTE", "A Razão Social do credor não pode exceder 150 caracteres.");
        validationResult.AddErrorIf(dto.CNPJ == 0, "CNPJ_INVALIDO", "O CNPJ do credor precisa ser válido.");
    }
}
