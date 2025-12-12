import { Injectable } from "@angular/core";
import { firstValueFrom } from "rxjs";
import { EntityService } from "@/core/services/entity.service";
import { ApiResponse } from "@/core/types/api-response.type";
import { Arquivo } from "../models/arquivo.model";

@Injectable({
    providedIn: 'root'
})
/**
 * @author Marcelo M. de Castro
 * @summary Serviço para gerenciar operações de arquivo, como upload e download.
 */
export class ArquivoService extends EntityService<Arquivo> {
    /**
     * @summary Construtor da classe ArquivoService.
     */
    constructor() { 
        // Chama o construtor da classe base com o endpoint específico
        super('arquivo');
    }

    /** 
     * @summary Realiza o upload de um arquivo para o servidor.
     * @param file O arquivo a ser enviado.
     * @returns Promise<ApiResponse> contendo a resposta da API.
    */
    async upload(file: File): Promise<ApiResponse> {
        const formData = new FormData();

        formData.append('file', file);
        formData.append('dataDaUltimaModificacao', new Date(file.lastModified).toISOString());

        var result$ = this.http.post<ApiResponse>(`${this.apiUrl}/upload`, formData);            
        return await firstValueFrom(result$);
    }

    /**
     * @summary Realiza o download de um arquivo do servidor.
     * @param arquivoId ID do arquivo a ser baixado.
     * @returns Promise<Blob> contendo o arquivo baixado.
     */
    async download(arquivoId: number): Promise<Blob> {
        var result$ = this.http.get(`${this.apiUrl}/download/${arquivoId}`, {
            responseType: 'blob'
        });            
        return await firstValueFrom(result$);
    }   
}