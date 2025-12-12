import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BancoInfo } from '../models/banco-info.model';
import { firstValueFrom } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
/**
 * @author Marcelo M. de Castro
 * @summary Serviço para obter informações sobre bancos.
 * @description Este serviço permite a obtenção de informações sobre bancos disponíveis na API Brasil.
 * @example
 * const bancos = await bancoService.obterListaDeBancos();
 * @returns Promise<BancoInfo[]> contendo a lista de bancos disponíveis.
 * @version 1.0.0
 */
export class BancoService {
    http = inject(HttpClient);
    apiUrl: string = 'https://brasilapi.com.br/api/banks/v1';

    constructor() { }

    /**
     * @summary Obtém a lista de bancos disponíveis.
     * @returns Promise<BancoInfo[]> contendo a lista de bancos disponíveis.
     */
    async obterListaDeBancos(): Promise<BancoInfo[]> {
        const bancos$ = this.http.get<BancoInfo[]>(this.apiUrl);
        return await firstValueFrom(bancos$);     
    }

    
    /**
     * @summary Obtém informações de um banco pelo código.
     * @param codigo O código do banco a ser obtido.
     * @returns Promise<BancoInfo | null> contendo as informações do banco ou null se não encontrado.
     */
    async obterBancoPorCodigo(codigo: string): Promise<BancoInfo | null> {
        const bancos$ = this.http.get<BancoInfo>(`${this.apiUrl}/${codigo}`);
        return await firstValueFrom(bancos$);     
    }
}
