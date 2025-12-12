import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    standalone: true,
    name: 'cnpj'
})
/**
 * @author Marcelo M. de Castro
 * @summary Pipe para formatar CNPJ (Cadastro Nacional da Pessoa Jurídica) no padrão brasileiro.
 * @description Esta pipe formata uma string ou número representando um CNPJ no formato "XX.XXX.XXX/XXXX-XX".
 * Se o valor fornecido for nulo, indefinido ou não tiver 14 dígitos numéricos, retorna uma string vazia ou o valor original.
 * @version 1.0
 */
export class CnpjPipe implements PipeTransform {
    transform(value: string | null | undefined): string {
        if (!value) return '';

        const stringValue = String(value).padStart(14, '0');
        const numericValue = stringValue.replace(/\D/g, '');

        if (numericValue.length !== 14) return stringValue;

        return numericValue.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/, '$1.$2.$3/$4-$5');
    }    
}