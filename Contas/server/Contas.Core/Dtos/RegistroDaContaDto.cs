using Contas.Core.Entities;
using Contas.Core.Interfaces;
using Contas.Core.Mappings;
using static Contas.Core.Objects.Enumerations;

namespace Contas.Core.Dtos;

public class RegistroDaContaDto : IDto, IConvertibleToEntity<RegistroDaConta>
{
    public int Id { get; set; }
    public int CredorId { get; set; }
    public int PagadorId { get; set; }
    public int Mes { get; set; }
    public required int Ano { get; set; }
    public required DateTime DataDeVencimento { get; set; }
    public required string CodigoDeBarras { get; set; }
    public DateTime? DataDePagamento { get; set; }
    public decimal Valor { get; set; }
    public decimal? ValorDosJuros { get; set; }
    public decimal ValorTotal { get; set; }
    public decimal? ValorDoDesconto { get; set; }
    public string? Observacoes { get; set; }
    public DateTime DataDeCriacao { get; set; } = DateTime.Now;
    public DateTime DataDeAtualizacao { get; set; } = DateTime.Now;
    public StatusDaConta? Status { get; set; }
    public int? DiasParaVencer { get; set; }
    public int? DiasEmAtraso { get; set; }
    public CredorDto? Credor { get; set; }
    public PagadorDto? Pagador { get; set; }
    public string? Periodo { get; set; }
    public virtual List<ArquivoDoRegistroDaContaDto>? Arquivos { get; set; } = [];

    public RegistroDaConta ConvertToEntity() => this.ToEntity();       
}
