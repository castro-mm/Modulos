import { TableColumn } from '@/core/types/table-column.type';
import { CommonService } from '@/core/services/common.service';
import { inject, Pipe, PipeTransform } from '@angular/core';

@Pipe({
    standalone: true,
    name: 'customFormat'
})
/**
 * @author Marcelo M. de Castro
 * @summary Pipe customizada para formatação de diversos tipos de dados.
 * @description Esta pipe utiliza o CommonService para aplicar formatações específicas, como CNPJ, CPF, telefone, entre outros.
 * O nome da formatação é passado como argumento para a pipe.
 * @version 1.0
 */
export class CustomFormatPipe implements PipeTransform {
    transform(value: any, col: TableColumn): any {
        if (!value || !col.pipe) return null;
        switch (col.pipe) {
            case 'cnpj':
                return this.formatCnpj(String(value));
            case 'cpf':
                return this.formatCpf(String(value));
            case 'telefone':
                return this.formatTelefone(String(value));
            // Adicione outros pipes customizados aqui
            default:
                return value;
        }
    }

    private formatCnpj(value: string): string {
        if (!value) return '';        
        const cnpj = value.padStart(14, '0').replace(/\D/g, '');
        return cnpj.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/, '$1.$2.$3/$4-$5');
    }

    private formatCpf(value: string): string {
        if (!value) return '';
        const cpf = value.padStart(11, '0').replace(/\D/g, '');
        return cpf.replace(/^(\d{3})(\d{3})(\d{3})(\d{2})$/, '$1.$2.$3-$4');
    }

    private formatTelefone(value: string): string {
        if (!value) return '';
        const tel = value.replace(/\D/g, '');
        if (tel.length === 11) {
            return tel.replace(/^(\d{2})(\d{5})(\d{4})$/, '($1) $2-$3');
        }
        return tel.replace(/^(\d{2})(\d{4})(\d{4})$/, '($1) $2-$3');
    }
}