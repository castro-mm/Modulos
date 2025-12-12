/**
 * @author Marcelo M. de Castro
 * @summary Enumeração das modalidades de arquivo.
 * @description Esta enumeração define as diferentes modalidades de arquivos que podem ser associados a transações financeiras no sistema.
 * @enum {ModalidadeDeArquivo}
 * @returns {ModalidadeDeArquivo} A modalidade de arquivo correspondente.
 */
export enum ModalidadeDeArquivo {
    BoletoBancario = 1,
    ComprovanteDePagamento = 2,
    NotaFiscal = 3,
    Recibo = 4
}

/**
 * @author Marcelo M. de Castro
 * @summary Enumeração dos status da conta.
 * @description Esta enumeração define os possíveis status que uma conta pode ter no sistema, facilitando o controle e a gestão das contas.
 * @enum {StatusDaConta}
 * @returns {StatusDaConta} O status da conta correspondente.
 */
export enum StatusDaConta {
    Pendente = 0,
    Paga = 1,
    Vencida = 2,
    Cancelada = 3
}