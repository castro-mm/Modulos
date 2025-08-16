using Contas.Core.Entities.Base;

namespace Contas.Core.Entities;

public class RegistroDaConta : Entity
{
    public int CredorId { get; set; }
    public int PagadorId { get; set; }
    public int Mes { get; set; }
    public required int Ano { get; set; }
    public required DateTime DataDeVencimento { get; set; }
    public required string CodigoDeBarras { get; set; }
    public DateTime? DataDePagamento { get; set; }
    public decimal Valor { get; set; }
    public decimal? ValorDosJuros { get; set; }
    public decimal? ValorPago { get; set; }
    public decimal? ValorDoDesconto { get; set; }
    public required byte[] BoletoBancario { get; set; }
    public byte[]? ComprovanteDePagamento { get; set; }

    public required virtual Credor Credor { get; set; }
    public required virtual Pagador Pagador { get; set; }
}
