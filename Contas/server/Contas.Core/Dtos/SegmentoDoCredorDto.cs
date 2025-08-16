using System.ComponentModel.DataAnnotations;
using Contas.Core.Entities;
using Contas.Core.Extensions;
using Contas.Core.Interfaces;

namespace Contas.Core.Dtos;

public class SegmentoDoCredorDto : IDto, IConvertibleToEntity<SegmentoDoCredor>
{
    public int Id { get; set; } = 0;
    [Required]
    public string Nome { get; set; } = string.Empty;
    public DateTime DataDeCriacao { get; set; } = DateTime.Now;
    public DateTime DataDeAtualizacao { get; set; } = DateTime.Now;

    public SegmentoDoCredor ConvertToEntity() => this.ToEntity();
}