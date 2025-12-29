using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos.System;
using Contas.Core.Objects;

namespace Contas.Core.Businesses.Validators;

public class ArquivoValidator : Validator<ArquivoDto>, IArquivoValidator
{
    private ValidationResult validationResult = new();

    public override ValidationResult Validate(ArquivoDto? dto)
    {
        validationResult = base.Validate(dto);

        if (validationResult.IsValid) SetErrorsConditionally(dto!);

        return validationResult;
    }

    private void SetErrorsConditionally(ArquivoDto dto)
    {
        validationResult.AddErrorIf(string.IsNullOrWhiteSpace(dto.Nome), "NOME_ARQUIVO_OBRIGATORIO", "O nome do arquivo é obrigatório.");
        validationResult.AddErrorIf(string.IsNullOrWhiteSpace(dto.Extensao), "EXTENSAO_ARQUIVO_OBRIGATORIA", "A extensão do arquivo é obrigatória.");
        validationResult.AddErrorIf(string.IsNullOrWhiteSpace(dto.Tipo), "TIPO_ARQUIVO_OBRIGATORIO", "O tipo do arquivo é obrigatório.");
        validationResult.AddErrorIf(dto.Dados == null || dto.Dados.Length == 0, "DADOS_ARQUIVO_OBRIGATORIOS", "Os dados do arquivo são obrigatórios.");

    }
}
