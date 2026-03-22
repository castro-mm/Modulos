using Microsoft.AspNetCore.Identity;

namespace Contas.Core.Entities.System.Security;

public class ApplicationUser : IdentityUser<int>
{    
    public required string NomeCompleto { get; set; }
    public bool IsActive { get; set; } = true;
    public string? FotoUrl { get; set; }
    public bool MustChangePassword { get; set; } = false;
    public DateTime DataDeCriacao { get; set; } = DateTime.Now;
    public DateTime DataDeAtualizacao { get; set; } = DateTime.Now;
}