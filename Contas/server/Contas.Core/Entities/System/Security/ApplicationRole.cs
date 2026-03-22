using Microsoft.AspNetCore.Identity;

namespace Contas.Core.Entities.System.Security;

public class ApplicationRole : IdentityRole<int>
{
    public string? CriadoPor { get; set; }
    public DateTime DataDeCriacao { get; set; } = DateTime.Now;
    public DateTime DataDeAtualizacao { get; set; } = DateTime.Now;
}
