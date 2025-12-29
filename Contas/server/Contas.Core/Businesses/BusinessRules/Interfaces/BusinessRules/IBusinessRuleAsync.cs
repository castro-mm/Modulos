using Contas.Core.Entities.Base;

namespace Contas.Core.Interfaces.Businesses.BusinessRules;

/// <summary>
/// Regra de negócio assíncrona
/// </summary>
/// <typeparam name="T">Entidade</typeparam>
public interface IBusinessRuleAsync<T> where T : Entity
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
    /// Verifica se a regra de negócio é satisfeita de forma assíncrona
    /// </summary>
    /// <param name="entity">Indica a entidade do contexto</param>
    /// <returns>Resultado da verificação</returns>
    Task<bool> IsSatisfiedByAsync(T entity);
}