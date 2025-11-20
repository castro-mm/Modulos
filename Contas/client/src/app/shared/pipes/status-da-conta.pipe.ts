import { StatusDaConta } from '@/core/objects/enums';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    standalone: true,
    name: 'statusDaConta'
})
export class StatusDaContaPipe implements PipeTransform {
    transform(value: number | null | undefined): string {
        if (value === null || value === undefined) return '';

        const status = StatusDaConta[value];

        return status;
    }
}