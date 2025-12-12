using Contas.Core.Entities.System;
using Contas.Core.Helpers;
using Contas.Core.Interfaces;
using Contas.Core.Mappings;

namespace Contas.Core.Dtos.System;

public class LogDeErroDto : IDto, IConvertibleToEntity<LogDeErro>
{
    public int Id { get; set; }
    public required string Mensagem { get; set; }
    public required string Detalhes { get; set; }
    public required string Metodo { get; set; }
    public required string Caminho { get; set; }
    public required string Ip { get; set; } // IP do cliente que gerou o erro
    public required string Navegador { get; set; } // Informações do navegador do cliente
    public required DateTime DataDeCriacao { get; set; }
    public required DateTime DataDeAtualizacao { get; set; }
    public required string Usuario { get; set; } // Pode ser o ID do usuário ou outro identificador
    public required Guid TraceId { get; set; }
    public string Hash => HashMD5Helper.Encrypt(this);

    public LogDeErro ConvertToEntity() => this.ToEntity();
}