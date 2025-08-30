namespace Contas.Core.Extensions;

public static class EnumExtensions
{
    public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
    {
        if (Enum.IsDefined(typeof(TEnum), value))
            return (TEnum)(object)value;

        throw new ArgumentException($"Valor '{value}' não é válido para o enum '{typeof(TEnum).Name}'.");
    }
}