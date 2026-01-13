using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos;
using Contas.Core.Objects;
using Contas.Infrastructure.Data;
using Contas.Infrastructure.Services.Base;

namespace Contas.Infrastructure.Services.Businesses.Validators;

public class PagadorValidator : Validator<PagadorDto>, IPagadorValidator
{
    private readonly ContasContext _context;
    private ValidationResult validationResult = new();

    public PagadorValidator(ContasContext context)
    {
        _context = context;
    }

    public override ValidationResult Validate(PagadorDto? dto)
    {
        validationResult = base.Validate(dto);

        if(validationResult.IsValid) SetErrorsConditionally(dto!);

        return validationResult;
    }
    private void SetErrorsConditionally(PagadorDto dto)
    {
        validationResult.AddErrorIf(string.IsNullOrWhiteSpace(dto.Nome), "NOME_OBRIGATORIO", "O nome do pagador é obrigatório.");
        validationResult.AddErrorIf(dto.CPF == 0, "CPF_INVALIDO", "O CPF do pagador precisa ser válido.");
        validationResult.AddErrorIf(dto.Email != null && dto.Email.Length > 150, "EMAIL_EXCEDENTE", "O email do pagador não pode exceder 150 caracteres.");
        validationResult.AddErrorIf(string.IsNullOrEmpty(dto.Email), "EMAIL_OBRIGATORIO", "O email do pagador é obrigatório.");

        validationResult.AddErrorIf(
            _context.Pagadores.Any(p => p.CPF == dto.CPF && (p.Id != dto.Id || dto.Id == 0)),
            "CPF_JA_EXISTENTE",
            "Já existe um pagador cadastrado com este CPF."
        );
        validationResult.AddErrorIf(
            _context.Pagadores.Any(p => p.Email == dto.Email && (p.Id != dto.Id || dto.Id == 0)),    
            "EMAIL_JA_EXISTENTE",
            "Já existe um pagador cadastrado com este email."
        );
    }
}
