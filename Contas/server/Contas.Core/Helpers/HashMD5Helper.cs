using System.Security.Cryptography;
using System.Text;

namespace Contas.Core.Helpers;

public static class HashMD5Helper
{
    /// <summary>
    /// Serializa as informações do objeto em Hash MD5.
    /// </summary>
    /// <param name="obj">Objeto com as informações a serem serializadas.</param>
    /// <returns>Hash MD5 das informações contidas no objeto.</returns>
    public static string Encrypt(object obj)
    {
        var sb = new StringBuilder();
        var properties = obj.GetType().GetProperties();

        foreach (var property in properties)
        {
            if (property.Name != "Id" && property.Name != "Hash")
            {
                var value = property.GetValue(obj)?.ToString() ?? string.Empty;
                sb.Append(value);
            }
        }

        var inputBytes = Encoding.UTF8.GetBytes(sb.ToString());
        var hashBytes = MD5.HashData(inputBytes);

        return Convert.ToHexStringLower(hashBytes);
    }
}
