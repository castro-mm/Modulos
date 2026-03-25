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

// Informações de gastos mensais por credor no dashboard
// ---------------------------------------------------------------------------
export class GastoMensalPorCredor {
    ano: number = 0;
    anosDisponiveis: number[] = [];
    credores: CredorGastoMensal[] = [];
}

export class CredorGastoMensal {
    credorId: number = 0;
    nomeFantasia: string = '';
    valores: number[] = [];
}

// Informações de gastos por segmento do credor no dashboard
// ---------------------------------------------------------------------------
export class GastoPorSegmentoDoCredor {
    ano: number = 0;
    anosDisponiveis: number[] = [];
    segmentos: SegmentoGasto[] = [];
}

export class SegmentoGasto {
    segmentoDoCredorId: number = 0;
    nome: string = '';
    valorTotal: number = 0;
}