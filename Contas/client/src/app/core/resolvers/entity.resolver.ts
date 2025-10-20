import { ResolveFn } from '@angular/router';
import { ApiResponse } from '../models/api-response.model';
import { DATA_SERVICE_TOKEN } from '../services/data-service.token';
import { EntityService } from '../services/entity.service';
import { Entity } from '../models/entity.model';
import { inject } from '@angular/core';

export const entityResolver: ResolveFn<Promise<ApiResponse>> = async (route, state) => {
    const service = inject(DATA_SERVICE_TOKEN) as EntityService<Entity>;
    const id = route.paramMap.get('id');
    if (!id) {
        return { data: null, message: 'No ID provided' } as ApiResponse;
    }
    return await service.getById(+id);
};
