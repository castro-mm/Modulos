using System.Collections.Concurrent;

namespace Contas.Core.Helpers;

public static class FactoryHelper
{
    private static readonly ConcurrentDictionary<string, object> _instances = new();

    public static T CreateInstance<T>(params object[] args) where T : class
    {
        // Criar uma chave única baseada no tipo e nos parâmetros
        var type = typeof(T);
        var argsKey = args?.Length > 0 ? string.Join(",", args.Select(a => a?.GetHashCode().ToString() ?? "null")) : "empty";
        var key = $"{type.FullName}_{argsKey}";

        return (T)_instances.GetOrAdd(
            key,
            _ => {
                try
                {
                    return Activator.CreateInstance(type, args) ?? throw new InvalidOperationException($"Não foi possível criar a instância de {type.Name}");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Erro ao criar instância de {type.Name}: {ex.Message}", ex);
                }
            }
        );
    }
}
