using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos;
using Contas.Core.Objects;
using Contas.Infrastructure.Data;
using Contas.Infrastructure.Services.Base;

namespace Contas.Infrastructure.Services.Businesses.Validators;

public class RegistroDaContaValidator : Validator<RegistroDaContaDto>, IRegistroDaContaValidator
{
    private readonly ContasContext _context;
    private ValidationResult validationResult = new();

    public RegistroDaContaValidator(ContasContext context)
    {
        _context = context;
    }

    public override ValidationResult Validate(RegistroDaContaDto? dto)
    {
        validationResult = base.Validate(dto);

        if(validationResult.IsValid) SetErrorsConditionally(dto!);
        
        return validationResult;
    }

    private void SetErrorsConditionally(RegistroDaContaDto dto)
    {
        validationResult.AddErrorIf(dto.CredorId == 0, "CREDOR_OBRIGATORIO", "O credor é obrigatório.");
        validationResult.AddErrorIf(dto.PagadorId == 0, "PAGADOR_OBRIGATORIO", "O pagador é obrigatório.");
        validationResult.AddErrorIf(dto.Valor <= 0, "VALOR_INVALIDO", "O valor da conta deve ser maior que zero.");
        validationResult.AddErrorIf(dto.ValorTotal <= 0, "VALOR_TOTAL_INVALIDO", "O valor total da conta deve ser maior que zero.");
        validationResult.AddErrorIf(dto.DataDeVencimento == DateTime.MinValue, "DATA_VENCIMENTO_INVALIDA", "A data de vencimento é inválida."); 
        validationResult.AddErrorIf(dto.Observacoes != null && dto.Observacoes.Length > 250, "DESCRICAO_EXCEDENTE", "A descrição não pode exceder 250 caracteres.");
        validationResult.AddErrorIf(dto.DataDeVencimento < dto.DataDePagamento, "DATA_VENCIMENTO_ANTERIOR_A_DATA_PAGAMENTO", "A data de vencimento não pode ser anterior à data de pagamento.");

        // Regras de negócio adicionais
        if(validationResult.IsValid)
        {
            validationResult.AddErrorIf(
                _context.RegistrosDaConta
                    .Any(r => r.CodigoDeBarras == dto.CodigoDeBarras && (r.Id != dto.Id || dto.Id == 0)),
                "CODIGO_BARRAS_DUPLICADO",
                "Já existe um registro da conta cadastrado com este Código de Barras."    
            );            
        }
    }
}
