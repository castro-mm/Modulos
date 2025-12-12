using System.ComponentModel.DataAnnotations;
using Contas.Core.Entities;
using Contas.Core.Interfaces;
using Contas.Core.Mappings;

namespace Contas.Core.Dtos;

public class SegmentoDoCredorDto : IDto, IConvertibleToEntity<SegmentoDoCredor>
{
    public int Id { get; set; } = 0;
    public required string Nome { get; set; }
    public DateTime DataDeCriacao { get; set; } = DateTime.Now;
    public DateTime DataDeAtualizacao { get; set; } = DateTime.Now;
    public List<CredorDto>? Credores { get; set; }

    public SegmentoDoCredor ConvertToEntity() => this.ToEntity();
}