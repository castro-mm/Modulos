export type CodigoDeBarras = {
    tipo: 'bancario' | 'concessionaria' | 'invalido';
    valido: boolean;
    valor?: number;
    dataDeVencimento?: Date;
    banco?: string;
    codigoDoBanco?: string;
    mensagem?: string;    
    detalhes?: DetalhesDoCodigoDeBarras;
}

export type DetalhesDoCodigoDeBarras = {
    produto?: string;
    segmento?: string;
    empresa?: string;
    moeda?: string;
    fatorDeVencimento?: number;
}