using Contas.Core.Entities;
using Contas.Core.Interfaces;
using Contas.Core.Mappings;

namespace Contas.Core.Dtos;

public class PagadorDto : IDto, IConvertibleToEntity<Pagador>
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required long CPF { get; set; }
    public DateTime DataDeCriacao { get; set; } = DateTime.Now;
    public DateTime DataDeAtualizacao { get; set; } = DateTime.Now;
    public List<RegistroDaContaDto>? RegistrosDaConta { get; set; }

    public Pagador ConvertToEntity() => this.ToEntity();
}