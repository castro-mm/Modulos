/**
 * @author Marcelo M. de Castro
 * @summary Classe abstrata que define os atributos-base de uma entidade.
 * @description Esta classe abstrata serve como base para todas as entidades do sistema, fornecendo atributos comuns como id, data de criação e data de atualização.
 * @param {number} id - O identificador único da entidade.
 * @param {Date} dataDeCriacao - A data em que a entidade foi criada.
 * @param {Date} dataDeAtualizacao - A data da última atualização da entidade.
 * @returns {Entity} Uma instância da classe Entity representando a entidade.
 * @abstract
 * @protected
 */
export abstract class Entity {
    id: number = 0;
    dataDeCriacao: Date = new Date();
    dataDeAtualizacao: Date = new Date();
}