namespace Contas.Core.Interfaces;

public interface IDto
{
    int Id { get; set; }
    DateTime DataDeCriacao { get; set; }
    DateTime DataDeAtualizacao { get; set; }
}