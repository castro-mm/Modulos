namespace Contas.Core.Dtos.Security;

public class RoleDto 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? CriadoPor { get; set; }
    public DateTime DataDeCriacao { get; set; }
    public DateTime DataDeAtualizacao { get; set; }

}

