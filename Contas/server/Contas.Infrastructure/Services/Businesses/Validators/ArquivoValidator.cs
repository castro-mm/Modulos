using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos.System;
using Contas.Core.Objects;
using Contas.Infrastructure.Services.Base;
using Microsoft.AspNetCore.Http;

namespace Contas.Infrastructure.Services.Businesses.Validators;

public class ArquivoValidator : Validator<ArquivoDto>, IArquivoValidator
{
    private ValidationResult validationResult = new();

    public override ValidationResult Validate(ArquivoDto? dto)
    {
        validationResult = base.Validate(dto);

        if (validationResult.IsValid) SetErrorsConditionally(dto!);

        return validationResult;
    }

    public ValidationResult Validate(IFormFile file)
    {
        validationResult = new ValidationResult();

        validationResult.AddErrorIf(file == null, "ARQUIVO_OBRIGATORIO", "O arquivo é obrigatório.");
        validationResult.AddErrorIf(file != null && file.Length == 0, "ARQUIVO_VAZIO", "O arquivo não pode estar vazio.");
        validationResult.AddErrorIf(file != null && file.Length > 1048576, "TAMANHO_ARQUIVO_EXCEDIDO", "O tamanho do arquivo não pode exceder 1MB.");

        return validationResult;
    }

    private void SetErrorsConditionally(ArquivoDto dto)
    {
        validationResult.AddErrorIf(string.IsNullOrWhiteSpace(dto.Nome), "NOME_ARQUIVO_OBRIGATORIO", "O nome do arquivo é obrigatório.");
        validationResult.AddErrorIf(string.IsNullOrWhiteSpace(dto.Extensao), "EXTENSAO_ARQUIVO_OBRIGATORIA", "A extensão do arquivo é obrigatória.");
        validationResult.AddErrorIf(string.IsNullOrWhiteSpace(dto.Tipo), "TIPO_ARQUIVO_OBRIGATORIO", "O tipo do arquivo é obrigatório.");
        validationResult.AddErrorIf(dto.Dados == null || dto.Dados.Length == 0, "DADOS_ARQUIVO_OBRIGATORIOS", "Os dados do arquivo são obrigatórios.");
        validationResult.AddErrorIf(dto.Tamanho <= 0, "TAMANHO_ARQUIVO_OBRIGATORIO", "O tamanho do arquivo deve ser maior que zero.");
        validationResult.AddErrorIf(dto.Tamanho > 1048576, "TAMANHO_ARQUIVO_EXCEDIDO", "O tamanho do arquivo não pode exceder 1MB.");
    }
}
