namespace Contas.Core.Dtos.Dahsboard;

public class GastoMensalPorCredorDto
{
    public int Ano { get; set; }
    public List<int> AnosDisponiveis { get; set; } = [];
    public List<CredorGastoMensalDto> Credores { get; set; } = [];
}

public class CredorGastoMensalDto
{
    public int CredorId { get; set; }
    public string NomeFantasia { get; set; } = string.Empty;
    public decimal[] Valores { get; set; } = new decimal[12];
}
