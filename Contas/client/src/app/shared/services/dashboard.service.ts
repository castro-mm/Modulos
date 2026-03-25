import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "src/environments/environment.development";
import { ApiResponse } from "@/core/types/api-response.type";
import { firstValueFrom } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class DashboardService {
    http = inject(HttpClient);
    apiUrl: string = environment.url + '/dashboard';
    
    constructor() { }

    async getQuantitativoDeContas(): Promise<ApiResponse> {
        const response$ = this.http.get<ApiResponse>(this.apiUrl + '/quantitativo-de-contas');
        return await firstValueFrom(response$);
    }

    async getGastoMensalPorCredor(ano: number): Promise<ApiResponse> {
        const response$ = this.http.get<ApiResponse>(this.apiUrl + '/gasto-mensal-por-credor', {
            params: { ano: ano.toString() }
        });
        return await firstValueFrom(response$);
    }

    async getGastoPorSegmentoDoCredor(ano: number): Promise<ApiResponse> {
        const response$ = this.http.get<ApiResponse>(this.apiUrl + '/gasto-por-segmento-do-credor', {
            params: { ano: ano.toString() }
        });
        return await firstValueFrom(response$);
    }
}