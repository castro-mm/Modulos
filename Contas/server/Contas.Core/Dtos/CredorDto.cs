using Contas.Core.Entities;
using Contas.Core.Extensions;
using Contas.Core.Interfaces;

namespace Contas.Core.Dtos;

public class CredorDto : IDto, IConvertibleToEntity<Credor>
{
    public int Id { get; set; }
    public required int SegmentoDoCredorId { get; set; }
    public required string RazaoSocial { get; set; }
    public required string NomeFantasia { get; set; }
    public required long CNPJ { get; set; }
    public DateTime DataDeCriacao { get; set; } = DateTime.Now;
    public DateTime DataDeAtualizacao { get; set; } = DateTime.Now;
    public SegmentoDoCredorDto? SegmentoDoCredor { get; set; }
    public List<RegistroDaContaDto>? RegistrosDaConta { get; set; }

    public Credor ConvertToEntity() => this.ToEntity();
}
