using Contas.Core.Entities;
using Contas.Core.Extensions;
using Contas.Core.Interfaces;
using static Contas.Core.Objects.Enums;

namespace Contas.Core.Dtos;

public class ArquivoDto : IDto, IConvertibleToEntity<Arquivo>
{
    public int Id { get; set; }
    public int RegistroDaContaId { get; set; }
    public required string Nome { get; set; }
    public required string Extensao { get; set; }
    public long Tamanho { get; set; }
    public required byte[] Conteudo { get; set; }
    public TipoDeArquivo Tipo { get; set; }
    public DateTime DataDeCriacao { get; set; } = DateTime.Now;
    public DateTime DataDeAtualizacao { get; set; } = DateTime.Now;

    public Arquivo ConvertToEntity() => this.ToEntity();
}
