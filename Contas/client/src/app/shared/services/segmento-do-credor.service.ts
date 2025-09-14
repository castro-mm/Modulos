import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { ApiResponse } from '@/core/models/api-response.model';
import { SegmentoDoCredorParams } from '../params/segmento-do-credor.params';
import { SegmentoDoCredor } from '../models/segmento-do-credor.model';

@Injectable({
    providedIn: 'root'
})
export class SegmentoDoCredorService {
    apiUrl: string = '';

    constructor(private http: HttpClient) { 
        this.apiUrl = `${environment.url}/segmentodocredor`;
    }

    async get(): Promise<ApiResponse> {
        const response$ = this.http.get<ApiResponse>(`${this.apiUrl}`);

        return await firstValueFrom(response$);
    }

    async getByParams(params: SegmentoDoCredorParams | null = null): Promise<ApiResponse> {
        const response$ = this.http.get<ApiResponse>(`${this.apiUrl}/get-by-params`, { params: { ...params } });

        return await firstValueFrom(response$);
    }

    async getById(id: number): Promise<ApiResponse> {
        const response$ = this.http.get<ApiResponse>(`${this.apiUrl}/${id}`);

        return await firstValueFrom(response$);        
    }

    async create(item: SegmentoDoCredor): Promise<ApiResponse> {
        const response$ = this.http.post<ApiResponse>(this.apiUrl, item);
        
        return await firstValueFrom(response$);
    }

    async update(id: number, changedItem: SegmentoDoCredor): Promise<ApiResponse> {
        const response$ = this.http.put<ApiResponse>(`${this.apiUrl}/${id}`, changedItem);

        return await firstValueFrom(response$)
    }

    async delete(id: number): Promise<ApiResponse> {
        const response$ = this.http.delete<ApiResponse>(`${this.apiUrl}/${id}`);

        return await firstValueFrom(response$);
    } 
    
    async deleteRange(ids: number[]): Promise<ApiResponse> {
        const response$ = this.http.delete<ApiResponse>(`${this.apiUrl}/delete-range`, { body: ids });

        return await firstValueFrom(response$);
    }
}
