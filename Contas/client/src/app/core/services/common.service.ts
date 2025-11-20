import { Injectable } from "@angular/core";
import { KeyValuePair } from "../models/key-value-pair.model";

@Injectable({
    providedIn: 'root'
})
export class CommonService {
    
    constructor() { }

    getMonthsList(): KeyValuePair[] {
        return [
            { key: 1, value: 'Janeiro' },
            { key: 2, value: 'Fevereiro' },
            { key: 3, value: 'Março' },
            { key: 4, value: 'Abril' },
            { key: 5, value: 'Maio' },
            { key: 6, value: 'Junho' },
            { key: 7, value: 'Julho' },
            { key: 8, value: 'Agosto' },
            { key: 9, value: 'Setembro' },
            { key: 10, value: 'Outubro' },
            { key: 11, value: 'Novembro' },
            { key: 12, value: 'Dezembro' }
        ];
    }

    getYearsList(startYear: number): KeyValuePair[] {
        const years: KeyValuePair[] = [];
        const currentYear = new Date().getFullYear();

        for (let year = currentYear; year >= startYear; year--) {
            years.push({ key: year, value: year.toString() });
        }
        return years;
    }

    getStatusDaContaOptions(): KeyValuePair[] {
        return [
            { key: 0, value: 'Pendente' },
            { key: 1, value: 'Paga' },
            { key: 2, value: 'Vencida' },
            { key: 99, value: 'Todos' }
        ];
    }
}