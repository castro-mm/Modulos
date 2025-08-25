using System.Collections.Concurrent;
using Contas.Core.Entities.Base;

namespace Contas.Core.Helpers;

public static class FactoryHelper
{
    private static ConcurrentDictionary<string, object> keyValuePairs = new();

    public static T CreateInstance<T>(params object[] args) where T : class
    {
        keyValuePairs = new();

        var type = typeof(T).Name;
        return (T)keyValuePairs.GetOrAdd(type, t =>
        {
            var tType = typeof(T);
            return Activator.CreateInstance(tType, args) ?? throw new InvalidOperationException($"Não foi possível criar a instancia de {t}");
        });
    }
}
