using Contas.Core.Entities;
using Contas.Core.Interfaces;
using Contas.Core.Mappings;
using static Contas.Core.Objects.Enumerations;

namespace Contas.Core.Dtos;

public class ArquivoDoRegistroDaContaDto : IDto, IConvertibleToEntity<ArquivoDoRegistroDaConta>
{
    public int Id { get; set; }
    public required int RegistroDaContaId { get; set; }
    public required int ArquivoId { get; set; }
    public required ModalidadeDoArquivo ModalidadeDoArquivo { get; set; }
    public DateTime DataDeCriacao { get; set; }
    public DateTime DataDeAtualizacao { get; set; }

    public RegistroDaContaDto? RegistroDaConta { get; set; }
    public System.ArquivoDto? Arquivo { get; set; }

    public ArquivoDoRegistroDaConta ConvertToEntity() => this.ToEntity();
}

