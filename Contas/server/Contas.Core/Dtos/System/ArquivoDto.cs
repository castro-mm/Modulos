using Contas.Core.Entities.System;
using Contas.Core.Interfaces;
using Contas.Core.Mappings;

namespace Contas.Core.Dtos.System;

public class ArquivoDto : IDto, IConvertibleToEntity<Arquivo>
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Extensao { get; set; }
    public required long Tamanho { get; set; }
    public required string Tipo { get; set; }
    public required byte[] Dados { get; set; }     
    public required DateTime DataDaUltimaModificacao { get; set; }
    public DateTime DataDeCriacao { get; set; }
    public DateTime DataDeAtualizacao { get; set; }

    public ArquivoDoRegistroDaContaDto? ArquivoDoRegistroDaConta { get; set; }

    Arquivo IConvertibleToEntity<Arquivo>.ConvertToEntity() => this.ToEntity();
}