// Informações do quantitativo de contas no dashboard
// ---------------------------------------------------------------------------
export class QuantitativoDeContas {
    contasAbertas?: SumarioDasContas;
    contasVencidas?: SumarioDasContas;
    contasPagas?: SumarioDasContas;
    contasQueVencemHoje?: SumarioDasContas;    
}
export class SumarioDasContas {
    quantidade: number = 0;
    valor: number = 0;
    percentual: number = 0;
    valorMedio: number = 0;
}