import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    standalone: true,
    name: 'cpf'
})
/**
 * @author Marcelo M. de Castro
 * @summary Pipe para formatar CPF (Cadastro de Pessoas Físicas) no padrão brasileiro.
 * @description Esta pipe formata uma string ou número representando um CPF no formato "XXX.XXX.XXX-XX".
 * Se o valor fornecido for nulo, indefinido ou não tiver 11 dígitos numéricos, retorna uma string vazia ou o valor original.
 * @version 1.0
 */
export class CpfPipe implements PipeTransform {
    transform(value: string | null | undefined): string {
        if (!value) return '';

        const stringValue = String(value).padStart(11, '0');
        const numericValue = stringValue.replace(/\D/g, '');

        if (numericValue.length !== 11) return stringValue;

        return numericValue.replace(/^(\d{3})(\d{3})(\d{3})(\d{2})$/, '$1.$2.$3-$4');
    }
}