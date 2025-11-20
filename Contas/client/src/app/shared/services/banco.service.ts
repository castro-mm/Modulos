import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BancoInfo } from '../models/banco-info.model';
import { firstValueFrom } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class BancoService {
    http = inject(HttpClient);
    apiUrl: string = 'https://brasilapi.com.br/api/banks/v1';

    constructor() { }

    async obterListaDeBancos(): Promise<BancoInfo[]> {
        const bancos$ = this.http.get<BancoInfo[]>(this.apiUrl);
        return await firstValueFrom(bancos$);     
    }

    async obterBancoPorCodigo(codigo: string): Promise<BancoInfo | null> {
        const bancos$ = this.http.get<BancoInfo>(`${this.apiUrl}/${codigo}`);
        return await firstValueFrom(bancos$);     
    }
}
