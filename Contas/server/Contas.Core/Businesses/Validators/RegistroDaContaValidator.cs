using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos;
using Contas.Core.Objects;

namespace Contas.Core.Businesses.Validators;

public class RegistroDaContaValidator : Validator<RegistroDaContaDto>, IRegistroDaContaValidator
{
    private ValidationResult validationResult = new();

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
        validationResult.AddErrorIf(dto.DataDePagamento != null && dto.DataDePagamento < dto.DataDeVencimento, "DATA_PAGAMENTO_INVALIDA", "A data de pagamento não pode ser anterior à data de vencimento.");
        validationResult.AddErrorIf(dto.Observacoes != null && dto.Observacoes.Length > 250, "DESCRICAO_EXCEDENTE", "A descrição não pode exceder 250 caracteres.");
    }
}
