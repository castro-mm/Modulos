namespace Contas.Core.Dtos.Dahsboard;

public class GastoPorSegmentoDoCredorDto
{
    public int Ano { get; set; }
    public List<int> AnosDisponiveis { get; set; } = [];
    public List<SegmentoGastoDto> Segmentos { get; set; } = [];
}

public class SegmentoGastoDto
{
    public int SegmentoDoCredorId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal ValorTotal { get; set; }
}
