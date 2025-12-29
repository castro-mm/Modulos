using Contas.Core.Entities.Base;

namespace Contas.Core.Interfaces.Businesses.BusinessRules;

/// <summary>
/// Regra de negócio síncrona
/// </summary>
/// <typeparam name="T">Entidade</typeparam>
public interface IBusinessRule<T> where T : Entity
{
    /// <summary>
    /// Código da regra de negócio
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Mensagem da regra de negócio
    /// </summary>
    string Message { get; }

    /// <summary>
    /// Verifica se a regra de negócio é satisfeita
    /// </summary>
    /// <param name="entity">Indica a entidade do contexto</param>
    /// <returns>Resultado da verificação</returns>
    bool IsSatisfiedBy(T entity);
}