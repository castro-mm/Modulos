import { ResolveFn } from '@angular/router';
import { ApiResponse } from '../types/api-response.type';
import { DATA_SERVICE_TOKEN } from '../services/data-service.token';
import { EntityService } from '../services/entity.service';
import { Entity } from '../models/entity.model';
import { inject } from '@angular/core';

/**
 * @author Marcelo M. de Castro
 * @summary Resolver para carregar uma entidade pelo seu ID.
 * @description Este resolver utiliza o serviço de entidade para buscar uma entidade específica com base no ID fornecido na rota. Ele retorna uma promise que resolve para um objeto ApiResponse contendo a entidade ou uma mensagem de erro se o ID não for fornecido.
 * @param route 
 * @param state 
 * @returns {Promise<ApiResponse>} Um objeto representando a resposta da API.
 */
export const entityResolver: ResolveFn<Promise<ApiResponse>> = async (route, state): Promise<ApiResponse> => {
    const service = inject(DATA_SERVICE_TOKEN) as EntityService<Entity>;
    const id = route.paramMap.get('id');
    if (!id) {
        return { data: null, message: 'No ID provided' } as ApiResponse;
    }
    return await service.getById(+id);
};
