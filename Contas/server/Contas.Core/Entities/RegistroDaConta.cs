using Contas.Core.Dtos;
using Contas.Core.Entities.Base;
using Contas.Core.Interfaces;
using Contas.Core.Mappings;

namespace Contas.Core.Entities;

public class RegistroDaConta : Entity, IConvertibleToDto<RegistroDaContaDto>
{
    public required int CredorId { get; set; }
    public required int PagadorId { get; set; }
    public required int Mes { get; set; }
    public required int Ano { get; set; }
    public required DateTime DataDeVencimento { get; set; }
    public required string CodigoDeBarras { get; set; }
    public DateTime? DataDePagamento { get; set; }
    public decimal Valor { get; set; }
    public decimal? ValorDosJuros { get; set; }
    public decimal? ValorDoDesconto { get; set; }
    public decimal ValorTotal { get; set; }
    public string? Observacoes { get; set; }

    public virtual Credor Credor { get; set; } = null!;
    public virtual Pagador Pagador { get; set; } = null!;
    public virtual ICollection<ArquivoDoRegistroDaConta> ArquivosDoRegistroDaConta { get; set; } = [];

    public RegistroDaContaDto ConvertToDto() => this.ToDto();
    public void ConvertFromDto(RegistroDaContaDto dto) => this.FromDto(dto);
}