using Contas.Core.Dtos;
using Contas.Core.Entities.Base;
using Contas.Core.Extensions;
using Contas.Core.Interfaces;

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

    public required virtual Credor Credor { get; set; }
    public required virtual Pagador Pagador { get; set; }
    public virtual ICollection<Arquivo> Arquivos { get; set; } = [];

    public RegistroDaContaDto ConvertToDto() => this.ToDto();
    public void ConvertFromDto(RegistroDaContaDto dto) => this.FromDto(dto);
}
