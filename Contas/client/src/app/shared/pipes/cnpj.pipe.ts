import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    standalone: true,
    name: 'cnpj'
})
export class CnpjPipe implements PipeTransform {
    transform(value: string | null | undefined): string {
        if (!value) return '';

        const stringValue = String(value);
        const numericValue = stringValue.replace(/\D/g, '');

        if (numericValue.length !== 14) return stringValue;

        return numericValue.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/, '$1.$2.$3/$4-$5');
    }
}