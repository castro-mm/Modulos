import { Injectable } from "@angular/core";
import { SelectOption } from "../types/select-option.type";

@Injectable({
    providedIn: 'root'
})
/**
 * @author Marcelo M. de Castro
 * @summary Serviço comum com utilitários compartilhados.
 * @description Este serviço fornece métodos utilitários comuns que podem ser usados em toda a aplicação, como listas de meses, anos e status de conta.
 * @version 1.0.0
 */
export class CommonService {
    constructor() { }

    /**
     * @summary Obtém uma lista de meses do ano.
     * @returns Lista de meses do ano como opções selecionáveis.
     * @example
     * const months = this.commonService.getMonthsList();
     */
    getMonthsList(): SelectOption[] {
        return [
            { value: 1, label: 'Janeiro', icon: '' },
            { value: 2, label: 'Fevereiro', icon: '' },
            { value: 3, label: 'Março', icon: '' },
            { value: 4, label: 'Abril', icon: '' },
            { value: 5, label: 'Maio', icon: '' },
            { value: 6, label: 'Junho', icon: '' },
            { value: 7, label: 'Julho', icon: '' },
            { value: 8, label: 'Agosto', icon: '' },
            { value: 9, label: 'Setembro', icon: '' },
            { value: 10, label: 'Outubro', icon: '' },
            { value: 11, label: 'Novembro', icon: '' },
            { value: 12, label: 'Dezembro', icon: '' }
        ];
    }

    /**
     * @summary Obtém uma lista de anos a partir de um ano inicial.
     * @param startYear Ano inicial para a geração da lista.
     * @returns Lista de anos a partir do ano inicial como opções selecionáveis.
     * @example
     * const years = this.commonService.getYearsList(2000);
     */
    getYearsList(startYear: number): SelectOption[] {
        const years: SelectOption[] = [];
        const currentYear = new Date().getFullYear();

        for (let year = currentYear; year >= startYear; year--) {
            years.push({ value: year, label: year.toString(), icon: '' });
        }
        return years;
    }
    
    /**
     * @summary Obtém as opções de status da conta.
     * @returns Lista de opções de status da conta.
     * @example
     * const statusOptions = this.commonService.getStatusDaContaOptions();
     */
    getStatusDaContaOptions(): SelectOption[] {
        return [
            { value: 0, label: 'Pendente', icon: 'pi pi-clock' },
            { value: 1, label: 'Paga', icon: 'pi pi-check' },
            { value: 2, label: 'Vencida', icon: 'pi pi-exclamation-triangle' },
            { value: 99, label: 'Todos', icon: 'pi pi-list' }
        ];
    }
   
    /**
     * @summary Obtém os tipos de arquivos permitidos para upload.
     * @param extensao Extensão do arquivo (ex: .pdf, .docx).
     * @returns Tipo MIME correspondente à extensão fornecida.
     * @example
     * const mimeType = this.commonService.getTiposDeArquivosPermitidos('.pdf');
     */ 
    getTiposDeArquivosPermitidos(extensao: string): string {
        const tiposDeArquivosPermitidos: { [key: string]: string } = {
            '.pdf': 'application/pdf',
            '.doc': 'application/msword',
            '.docx': 'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
            '.xls': 'application/vnd.ms-excel',
            '.xlsx': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
            '.png': 'image/png',
            '.jpg': 'image/jpeg',
            '.jpeg': 'image/jpeg',
            '.gif': 'image/gif'
        };

        return tiposDeArquivosPermitidos[extensao] || 'Tipo de arquivo não permitido';
    }

    /**
     * @summary Verifica se um valor é uma string de data ISO.
     * @description Identifica strings no formato ISO (yyyy-MM-dd ou yyyy-MM-ddTHH:mm:ss).
     * @param value Valor a ser verificado.
     * @returns true se for uma string de data ISO, false caso contrário.
     */
    isDateString(value: any): boolean {
        if (!value || typeof value !== 'string') return false;

        // Verificar se é uma string ISO com ou sem hora
        return value.match(/^\d{4}-\d{2}-\d{2}(T|$)/) !== null;
    }

    /**
     * @summary Verifica se um valor é uma data válida (Date object).
     * @description Identifica Date objects que precisam ser convertidos para string.
     * @param value Valor a ser verificado.
     * @returns true se for um Date object válido, false caso contrário.
     */
    isDateValue(value: any): boolean {
        if (!value) return false;

        // Verificar se é um Date object válido
        return value instanceof Date && !isNaN(value.getTime());
    }

    /**
     * @summary Converte uma data para o formato yyyy-MM-dd.
     * @description Extrai apenas a parte da data (sem hora) de um Date object.
     * @param date Data a ser formatada (Date object).
     * @returns String no formato yyyy-MM-dd.
     */
    formatDateToISO(date: Date): string {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        
        return `${year}-${month}-${day}`;
    }
}