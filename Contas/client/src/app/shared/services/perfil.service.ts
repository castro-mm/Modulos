import { Injectable } from '@angular/core';
import { Perfil } from '../models/perfil.model';
import { EntityService } from '@/core/services/entity.service';
import { ApiResponse } from '@/core/types/api-response.type';
import { Params } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class PerfilService extends EntityService<Perfil> {
    constructor() {
        super('account/roles');
    }

    /**
     * @summary Sobrescreve getByParams para usar getAll, pois o backend de Roles não possui endpoint get-by-params.
     * Adapta a resposta para o formato { items: [...] } esperado pelo EntityListComponent.
     */
    override async getByParams(params: Params): Promise<ApiResponse> {
        const response = await this.getAll();

        // Adaptar formato: o backend retorna array direto em data, mas EntityListComponent espera data.items
        if (response.result?.isSuccessful && Array.isArray(response.result.data)) {
            response.result.data = { items: response.result.data };
        }

        return response;
    }
}
