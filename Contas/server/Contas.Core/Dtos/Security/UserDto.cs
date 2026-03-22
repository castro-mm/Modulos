using Contas.Core.Interfaces;

namespace Contas.Core.Dtos.Security;

public class UserDto
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool MustChangePassword { get; set; }
    public string Role { get; set; } = string.Empty;
    public string? FotoUrl { get; set; }
    public DateTime DataDeCriacao { get; set; }
    public DateTime DataDeAtualizacao { get; set; }
}
