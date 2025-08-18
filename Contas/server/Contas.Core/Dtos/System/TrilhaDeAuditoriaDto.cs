using Contas.Core.Entities.System;
using Contas.Core.Extensions;
using Contas.Core.Interfaces;

namespace Contas.Core.Dtos.System;

public class TrilhaDeAuditoriaDto : IDto, IConvertibleToEntity<TrilhaDeAuditoria>
{
    public int Id { get; set; }
    public required string Entidade { get; set; }
    public required string Metodo { get; set; }
    public required string Caminho { get; set; }
    public required string Operacao { get; set; }
    public string? ValoresAntigos { get; set; }
    public string? ValoresNovos { get; set; }
    public required string Ip { get; set; }
    public required string Navegador { get; set; }
    public DateTime DataDeCriacao { get; set; } = DateTime.Now;
    public DateTime DataDeAtualizacao { get; set; } = DateTime.Now;
    public required string Usuario { get; set; }
    public required Guid TraceId { get; set; }
    public required string Hash { get; set; }

    public TrilhaDeAuditoria ConvertToEntity() => this.ToEntity();
}

