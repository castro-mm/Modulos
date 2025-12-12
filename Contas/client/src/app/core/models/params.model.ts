/**
 * @author Marcelo M. de Castro
 * @summary Classe abstrata que define parâmetros de paginação.
 * @description Esta classe fornece propriedades para controle de paginação, como índice da página e tamanho da página.
 * @param {number} pageIndex - O índice da página atual.
 * @param {number} pageSize - O número de itens por página.
 * @returns {Params} Uma instância da classe Params representando os parâmetros de paginação.
 * @abstract
 * @protected
 */
export abstract class Params {
    pageIndex: number = 1;
    pageSize: number = 50;
}