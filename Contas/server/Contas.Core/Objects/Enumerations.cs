namespace Contas.Core.Objects;

public class Enumerations
{
    public enum ModalidadeDoArquivo
    {
        BoletoBancario = 1,
        ComprovanteDePagamento = 2,
        Outro = 3
    }

    public enum StatusDaConta
    {
        Pendente = 0,
        Paga = 1,
        Vencida = 2,
        Cancelada = 3,
        Todos = 99
    }
}

public static class EnumExtensions
{
    public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
    {
        if (Enum.IsDefined(typeof(TEnum), value))
            return (TEnum)(object)value;

        throw new ArgumentException($"Valor '{value}' não é válido para o enum '{typeof(TEnum).Name}'.");
    }
}
