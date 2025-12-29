import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { ApiResponse } from '@/core/types/api-response.type';
import { Params } from '@angular/router';
import { Entity } from '../models/entity.model';

@Injectable({
    providedIn: 'root'
})
/**
 * @author Marcelo M. de Castro
 * @summary Serviço genérico para operações CRUD em uma entidade.
 * @description Este serviço fornece métodos para criar, ler, atualizar e excluir entidades de forma genérica.
 * Ele pode ser estendido ou utilizado diretamente para qualquer entidade que siga a estrutura esperada.
 * @example
 * ```typescript
 * import { Injectable } from '@angular/core';
 * import { EntityService } from './entity.service';
 * import { User } from '../models/user.model';
 * 
 * @Injectable({
 *   providedIn: 'root'
 * })
 * export class UserService extends EntityService<User> {
 *   constructor() {
 *     super('users'); // 'users' é o caminho da entidade na API
 *   }
 * }
 * ```
 * @version 1.0.0
 * @template T Tipo da entidade que estende a classe Entity.
 * @protected
 * @abstract
 */
export abstract class EntityService<T extends Entity> {
    apiUrl: string = environment.url;
    http = inject(HttpClient);

    /**
     * @summary Construtor do serviço genérico de entidade.
     * @param entityPath Caminho da entidade na API (ex: 'users', 'products').
     */
    constructor(protected entityPath: string) { 
        this.apiUrl = `${environment.url}/${this.entityPath}`;
    }

    /**
     * @summary Obtém uma lista de entidades.
     * @returns Uma Promise que resolve para a resposta da API contendo uma lista de entidades.
     * @example
     * ```typescript
     * const response = await userService.get();
     * ```
     */
    async getAll(): Promise<ApiResponse> {
        const response$ = this.http.get<ApiResponse>(`${this.apiUrl}`);
        return await firstValueFrom(response$);
    }

    /**
     * @summary Obtém uma lista de entidades filtradas por parâmetros.
     * @param params Parâmetros de consulta para filtrar a lista de entidades.
     * @returns Uma Promise que resolve para a resposta da API contendo uma lista de entidades filtradas. 
     * @example
     * ```typescript
     * const response = await userService.getByParams({ role: 'admin', isActive: true });
     * ```
     */
    async getByParams(params: Params): Promise<ApiResponse> {
        const response$ = this.http.get<ApiResponse>(`${this.apiUrl}/get-by-params`, { params: { ...params } });
        return await firstValueFrom(response$);
    }

    /**
     * @summary Obtém uma entidade pelo seu ID.
     * @param id ID da entidade a ser obtida.
     * @returns Uma Promise que resolve para a resposta da API contendo a entidade solicitada.
     * @example
     * ```typescript
     * const response = await userService.getById(1);
     * ```
     */
    async getById(id: number): Promise<ApiResponse> {
        const response$ = this.http.get<ApiResponse>(`${this.apiUrl}/${id}`);
        return await firstValueFrom(response$);        
    }

    /**
     * @summary Cria uma nova entidade.
     * @param entity Entidade a ser criada.
     * @returns Uma Promise que resolve para a resposta da API após a criação da entidade.
     * @example
     * ```typescript
     * const newUser: User = { name: 'John Doe'};
     * const response = await userService.create(newUser);
     * ```    
     */
    async create(entity: T): Promise<ApiResponse> {
        const response$ = this.http.post<ApiResponse>(this.apiUrl, entity);
        return await firstValueFrom(response$);
    }

    /**
     * @summary Atualiza uma entidade existente.
     * @param id ID da entidade a ser atualizada.
     * @param changedEntity Entidade com os dados atualizados. 
     * @returns Uma Promise que resolve para a resposta da API após a atualização da entidade.
     * @example
     * ```typescript
     * const updatedUser: User = { id: 1, name: 'Jane Doe', email: '' };
     * const response = await userService.update(1, updatedUser);
     * ```
     */
    async update(id: number, changedEntity: T): Promise<ApiResponse> {
        const response$ = this.http.put<ApiResponse>(`${this.apiUrl}/${id}`, changedEntity);
        return await firstValueFrom(response$);
    }

    /**
     * @summary Exclui uma entidade pelo seu ID.
     * @param id ID da entidade a ser excluída.
     * @returns Uma Promise que resolve para a resposta da API após a exclusão da entidade. 
     * @example
     * ```typescript
     * const response = await userService.delete(1);
     * ```
     */ 
    async delete(id: number): Promise<ApiResponse> {
        const response$ = this.http.delete<ApiResponse>(`${this.apiUrl}/${id}`);
        return await firstValueFrom(response$);
    } 
    
    /**
     * @summary Exclui várias entidades pelos seus IDs.
     * @param ids Array de IDs das entidades a serem excluídas.
     * @returns Uma Promise que resolve para a resposta da API após a exclusão das entidades. 
     * @example
     * ```typescript
     * const response = await userService.deleteRange([1, 2, 3]);
     * ``` 
     */
    async deleteRange(ids: number[]): Promise<ApiResponse> {
        const response$ = this.http.delete<ApiResponse>(`${this.apiUrl}/delete-range`, { body: ids });
        return await firstValueFrom(response$);
    }
}
