/**
 * @author Marcelo M. de Castro
 * @summary Tipo que define o modelo para opções de seleção em dropdowns e selects.
 * @description Este tipo representa uma opção de seleção, incluindo um valor, um rótulo e um ícone opcional.
 * @param {any} value - O valor associado à opção de seleção.
 * @param {string} label - O rótulo exibido para a opção de seleção.
 * @param {string} [icon] - Um ícone opcional associado à opção de seleção.
 * @returns {SelectOption} Uma instância do tipo SelectOption representando a opção de seleção.
 */
export type SelectOption = {
    value: any;
    label: string;
    icon?: string;
}