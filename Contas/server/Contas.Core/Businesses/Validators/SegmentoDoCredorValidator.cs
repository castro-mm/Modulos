using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos;
using Contas.Core.Objects;

namespace Contas.Core.Businesses.Validators;

public class SegmentoDoCredorValidator : Validator<SegmentoDoCredorDto>, ISegmentoDoCredorValidator
{
    private ValidationResult validationResult = new();

    public override ValidationResult Validate(SegmentoDoCredorDto? dto)
    {
        validationResult = base.Validate(dto);

        if (validationResult.IsValid) SetErrorsConditionally(dto!);

        return validationResult;
    }

    private void SetErrorsConditionally(SegmentoDoCredorDto dto)
    {
        validationResult.AddErrorIf(string.IsNullOrWhiteSpace(dto.Nome), "NOME_OBRIGATORIO", "O nome do segmento do credor é obrigatório.");
        validationResult.AddErrorIf(!string.IsNullOrWhiteSpace(dto.Nome) && dto.Nome.Length < 3, "NOME_INVALIDO", "O nome do segmento do credor deve ter pelo menos 03 caracteres.");
        validationResult.AddErrorIf(!string.IsNullOrWhiteSpace(dto.Nome) && dto.Nome.Length > 100, "NOME_EXCEDENTE", "O nome do segmento do credor não pode exceder 100 caracteres.");
    }
}
