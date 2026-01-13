using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos;
using Contas.Core.Objects;
using Contas.Infrastructure.Data;
using Contas.Infrastructure.Services.Base;

namespace Contas.Infrastructure.Services.Businesses.Validators;

public class CredorValidator : Validator<CredorDto>, ICredorValidator
{
    private readonly ContasContext _context;
    private ValidationResult validationResult = new();

    public CredorValidator(ContasContext context)
    {
        _context = context;
    }

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

        validationResult.AddErrorIf(
            _context.Credores.Any(c => c.CNPJ == dto.CNPJ && (c.Id != dto.Id || dto.Id == 0)),
            "CNPJ_DUPLICADO", 
            "Já existe um credor cadastrado com este CNPJ."
        );
        validationResult.AddErrorIf(
            _context.Credores.Any(c => c.RazaoSocial.ToLower() == dto.RazaoSocial.ToLower() && (c.Id != dto.Id || dto.Id == 0)),
            "RAZAO_SOCIAL_DUPLICADA", 
            "Já existe um credor cadastrado com esta Razão Social.");
    }
}
