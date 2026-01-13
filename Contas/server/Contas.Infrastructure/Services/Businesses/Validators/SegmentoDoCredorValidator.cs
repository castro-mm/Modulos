using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos;
using Contas.Core.Objects;
using Contas.Infrastructure.Data;
using Contas.Infrastructure.Services.Base;

namespace Contas.Infrastructure.Services.Businesses.Validators;

public class SegmentoDoCredorValidator : Validator<SegmentoDoCredorDto>, ISegmentoDoCredorValidator
{
    private readonly ContasContext _context;
    private ValidationResult validationResult = new();

    public SegmentoDoCredorValidator(ContasContext context)
    {
        _context = context;
    }

    public override ValidationResult Validate(SegmentoDoCredorDto? dto)
    {
        validationResult = base.Validate(dto);

        if (validationResult.IsValid) SetErrorsConditionally(dto!);

        return validationResult;
    }

    private void SetErrorsConditionally(SegmentoDoCredorDto dto)
    {
        validationResult.AddErrorIf(string.IsNullOrWhiteSpace(dto.Nome), "NOME_OBRIGATORIO", "O nome do segmento do credor é obrigatório.");
        if (!validationResult.IsValid) return;

        validationResult.AddErrorIf(!string.IsNullOrWhiteSpace(dto.Nome) && dto.Nome.Length < 3, "NOME_INVALIDO", "O nome do segmento do credor deve ter pelo menos 03 caracteres.");
        validationResult.AddErrorIf(!string.IsNullOrWhiteSpace(dto.Nome) && dto.Nome.Length > 100, "NOME_EXCEDENTE", "O nome do segmento do credor não pode exceder 100 caracteres.");
        validationResult.AddErrorIf(!string.IsNullOrWhiteSpace(dto.Nome) && !dto.Nome.All(char.IsLetterOrDigit), "NOME_INVALIDO_CARACTERES", "O nome do segmento do credor não pode conter caracteres especiais.");

        validationResult.AddErrorIf(
            _context.SegmentosDoCredor.Any(s => s.Nome.ToLower() == dto.Nome.ToLower() && (s.Id != dto.Id || dto.Id == 0)), 
            "NOME_JA_EXISTENTE", 
            $"Já existe um segmento do credor com o nome '{dto.Nome}' cadastrado.");
    }
}
