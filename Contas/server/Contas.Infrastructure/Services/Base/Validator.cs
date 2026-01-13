using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Interfaces;
using Contas.Core.Objects;
using Contas.Core.Specifications.Base;

namespace Contas.Infrastructure.Services.Base;

/// <summary>
/// Validador genérico dos Dtos
/// </summary>
/// <typeparam name="T">Dto da entidade</typeparam>
public class Validator<T> : IValidator<T> where T : IDto
{
    /// <summary>
    /// Valida as informações da entidade. 
    /// Por default, verifica se o objeto é nulo.
    /// </summary>
    /// <param name="dto">Objeto Dto com as informações necessárias</param>
    /// <returns>O resultado da validação</returns>
    public virtual ValidationResult Validate(T? dto)
    {
        var validationResult = new ValidationResult();

        if (dto == null)
            validationResult.AddError("OBJETO_NULO", "O objeto não pode ser nulo.");

        return validationResult;
    }

    /// <summary>
    /// Valida os parâmetros de consulta
    /// </summary>
    /// <param name="specParams">Parametros de consulta</param>
    /// <returns>Resultado da validação, indicando se houveram definições de parametros</returns>
    public virtual ValidationResult Validate(SpecificationParams specParams)
    {
        var validationResult = new ValidationResult();

        if (specParams == null)
            validationResult.AddError("PARAMETROS_NAO_INFORMADOS", "Os parâmetros de consulta não foram informados.");
        else
        {
            if (specParams.PageIndex < 0)
                validationResult.AddError("PAGE_INDEX_INVALIDO", "O índice da página não pode ser negativo.");
            if (specParams.PageSize <= 0)
                validationResult.AddError("PAGE_SIZE_INVALIDO", "O tamanho da página deve ser maior que zero.");
        }

        return validationResult;
    }

    /// <summary>
    /// Valida o resultado paginado
    /// </summary>
    /// <param name="pagedResult">Resultado paginado</param>
    /// <returns>Resultado da validação, indicando se existem dados a serem retornados</returns>
    public virtual ValidationResult Validate(PagedResult<T> pagedResult)
    {
        var validationResult = new ValidationResult();

        if (pagedResult == null || pagedResult.Items == null || pagedResult.Count == 0)
            validationResult.AddError("REGISTROS_NAO_ENCONTRADOS", "Nenhum registro encontrado com os parâmetros informados.");

        return validationResult;
    }

    public virtual ValidationResult Validate(int id)
    {
        var validationResult = new ValidationResult();

        if (id <= 0)
            validationResult.AddError("ID_INVALIDO", "O ID do arquivo deve ser um número positivo.");

        return validationResult;
    }
}