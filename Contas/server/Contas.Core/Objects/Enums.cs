namespace Contas.Core.Objects;

public class Enums
{
    public enum TipoDeArquivo
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
        Cancelada = 4
    }
}
