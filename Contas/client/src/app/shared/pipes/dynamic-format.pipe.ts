import { TableColumn } from '@/core/types/table-column.type';
import { CurrencyPipe, DatePipe, DecimalPipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    standalone: true,
    name: 'dynamicFormat'
})
/**
 * @author Marcelo M. de Castro
 * @summary Pipe customizada para formatação dinâmica de diversos tipos de dados.
 * @description Esta pipe utiliza as pipes nativas do Angular (DatePipe, CurrencyPipe, DecimalPipe) para aplicar formatações específicas,
 * como data, moeda, número, entre outros. O tipo de formatação e o formato desejado são passados como argumentos para a pipe.
 * @version 1.0
 */
export class DynamicFormatPipe implements PipeTransform {
    private datePipe = new DatePipe('pt-BR');
    private decimalPipe = new DecimalPipe('pt-BR');
    private currencyPipe = new CurrencyPipe('pt-BR');

    transform(value: any, col: TableColumn): any {
        switch (col.type) {
            case 'date':
                return this.datePipe.transform(value, col.format || 'dd/MM/yyyy HH:mm:ss');
            case 'currency':
                return this.currencyPipe.transform(value, col.format || '1.2-2');
            case 'number':
                return this.decimalPipe.transform(value, col.format || '1.0-2');
            case 'boolean':
                return value ? 'Sim' : 'Não';
            default:
                return value;
        }
    }
}