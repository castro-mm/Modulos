using Contas.Core.Interfaces;
using Contas.Core.Objects;
using Contas.Core.Specifications.Base;

namespace Contas.Core.Businesses.Validators.Interfaces;

/// <summary>
/// Validador genérico para entidades
/// </summary>
/// <typeparam name="T">Entidade</typeparam>
public interface IValidator<T> where T : IDto
{
    /// <summary>
    /// Valida as informações da entidade
    /// </summary>
    /// <param name="dto">Indica o dto com as informações necessárias</param>
    /// <returns>Resultado da validação</returns>
    ValidationResult Validate(T? dto);

    /// <summary>
    /// Valida os parâmetros de consulta
    /// </summary>
    /// <param name="specParams">Parâmetros de consulta</param>
    /// <returns>Resultado da validação</returns>
    ValidationResult Validate(SpecificationParams specParams);

    /// <summary>
    /// Valida o resultado paginado
    /// </summary>
    /// <param name="pagedResult">Resultado paginado</param>
    /// <returns>Resultado da validação</returns>
    ValidationResult Validate(PagedResult<T> pagedResult);
}
