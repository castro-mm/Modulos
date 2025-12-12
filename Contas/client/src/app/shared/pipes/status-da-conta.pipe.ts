import { Pipe, PipeTransform } from '@angular/core';
import { StatusDaConta } from '../objects/enums';

@Pipe({
    standalone: true,
    name: 'statusDaConta'
})
/**
 * @author Marcelo M. de Castro
 * @summary Pipe customizada para exibição do status da conta.
 * @description Esta pipe converte o valor numérico do status da conta em uma string legível,
 * conforme definido no enum StatusDaConta.
 * @version 1.0
 */
export class StatusDaContaPipe implements PipeTransform {
    transform(value: number | null | undefined): string {
        if (value === null || value === undefined) return '';

        const status = StatusDaConta[value];

        return status !== undefined ? status : 'Status desconhecido';
    }
}