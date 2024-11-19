using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity;

public class AppUser : IdentityUser<int>
{
    public string Nome { get; set; } = string.Empty;
    public long Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public DateTime DataDoCadastro { get; set; } = DateTime.Now;
    public DateTime DataDoUltimoAcesso { get; set; } = DateTime.Now;

    public ICollection<AppUserRole>? UserRoles { get; set; }
}
