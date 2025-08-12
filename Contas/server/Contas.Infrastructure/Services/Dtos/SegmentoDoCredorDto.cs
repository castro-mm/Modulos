using System.ComponentModel.DataAnnotations;

namespace Contas.Infrastructure.Services.Dtos;

public class SegmentoDoCredorDto
{
    public int Id { get; set; } = 0;
    [Required]
    public string Nome { get; set; } = string.Empty;
    public DateTime DataDeCriacao { get; set; } = DateTime.Now;
    public DateTime DataDeAtualizacao { get; set; } = DateTime.Now;
}