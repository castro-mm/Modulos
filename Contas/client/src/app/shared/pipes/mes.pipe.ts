import { CommonService } from '@/core/services/common.service';
import { inject, Pipe, PipeTransform } from '@angular/core';

@Pipe({
    standalone: true,
    name: 'mes'
})
export class MesPipe implements PipeTransform {
    commonService = inject(CommonService);

    transform(value: number | null | undefined): string {
        if (!value) return '';

        const mes = this.commonService.getMonthsList().find(m => m.key === value);        

        return mes ? mes.value : '';
    }
}