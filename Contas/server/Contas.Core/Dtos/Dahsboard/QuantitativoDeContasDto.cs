namespace Contas.Core.Dtos.Dahsboard;

public class QuantitativoDeContasDto
{
    public SumarioDasContas ContasAbertas { get; set; } = new();
    public SumarioDasContas ContasVencidas { get; set; } = new();
    public SumarioDasContas ContasPagas { get; set; } = new();
    public SumarioDasContas ContasQueVencemHoje { get; set; } = new();
}

public class SumarioDasContas
{
    public int Quantidade { get; set; } = 0;
    public decimal Valor { get; set; } = 0m;
    public decimal Percentual { get; set; } = 0m;
    public decimal ValorMedio { get; set; } = 0m;
}   
