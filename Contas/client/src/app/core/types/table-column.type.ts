export type ColumnType = 'text' | 'date' | 'currency' | 'number' | 'custom' | 'boolean';
export type AlignType = 'left' | 'center' | 'right';

/**
 * @author Marcelo M. de Castro
 * @summary Tipo que define as propriedades de uma coluna de tabela.
 * @description Este tipo representa as propriedades necessárias para configurar uma coluna em uma tabela, incluindo campo, cabeçalho, tipo de dado, formatação, alinhamento e opções de ordenação e filtragem.
 * @param {string} field - O campo associado à coluna.
 * @param {string} header - O cabeçalho exibido para a coluna.
 * @param {ColumnType} [type] - O tipo de dado para formatação.
 * @param {string} [format] - O formato específico para a coluna.
 * @param {string} [pipe] - O nome do pipe customizado associado à coluna.
 * @param {string} [width] - A largura da coluna.
 * @param {AlignType} [align] - O alinhamento do conteúdo da coluna.
 * @param {boolean} [sortable] - Se a coluna é ordenável.
 * @param {boolean} [filterable] - Se a coluna pode ser filtrada.
 * @param {string} [template] - O template associado à coluna.
 * @param {boolean} [editable] - Se a coluna é editável.
 * @returns {TableColumn} Uma instância da interface TableColumn representando a coluna da tabela.
 */
export type TableColumn = {
    field: string;          // Campo simples ou aninhado (ex: 'segmentoDoCredor.nome')
    header: string;         // Título da coluna
    type?: ColumnType;      // Tipo de dado para formatação
    format?: string;        // Formato específico (ex: 'dd/MM/yyyy HH:mm:ss' para datas)
    pipe?: string;          // Nome do pipe customizado (ex: 'cnpj', 'cpf')
    width?: string;         // Largura da coluna (ex: '10rem')
    align?: AlignType;      // Alinhamento do conteúdo
    sortable?: boolean;     // Se a coluna é ordenável (default: true)
    filterable?: boolean;   // Se a coluna pode ser filtrada (default: true)
    template?: string;      // Template associado à coluna              
    editable?: boolean;     // Se a coluna é editável (default: false)
}