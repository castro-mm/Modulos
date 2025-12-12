/**
 * @author Marcelo M. de Castro
 * @summary Tipo que define a estrutura do código de barras
 */
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

/**
 * @author Marcelo M. de Castro
 * @summary Tipo que define os detalhes adicionais do código de barras.
 */
export type DetalhesDoCodigoDeBarras = {
    produto?: string;
    segmento?: string;
    empresa?: string;
    moeda?: string;
    fatorDeVencimento?: number;
}