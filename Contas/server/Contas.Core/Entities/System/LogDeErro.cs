using Contas.Core.Dtos.System;
using Contas.Core.Entities.Base;
using Contas.Core.Extensions;
using Contas.Core.Interfaces;

namespace Contas.Core.Entities.System;

public class LogDeErro : Entity, IConvertibleToDto<LogDeErroDto>
{
    public required string Mensagem { get; set; }
    public required string Detalhes { get; set; }
    public required string Metodo { get; set; }
    public required string Caminho { get; set; }
    public required string Ip { get; set; } 
    public required string Navegador { get; set; } 
    public required string Usuario { get; set; } 
    public required Guid TraceId { get; set; }
    public required string Hash { get; set; }    

    public LogDeErroDto ConvertToDto() => this.ToDto();
    public void ConvertFromDto(LogDeErroDto dto) => this.FromDto(dto);
}
