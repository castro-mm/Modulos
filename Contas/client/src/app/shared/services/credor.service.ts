import { EntityService } from '@/core/services/entity.service';
import { Injectable } from '@angular/core';
import { Credor } from '../models/credor.model';

@Injectable({
    providedIn: 'root'
})
/**
 * @author Marcelo M. de Castro
 * @summary Serviço para gerenciar informações de credores.
 * @description Este serviço permite a manipulação de dados relacionados a credores.
 * @version 1.0.0
 */
export class CredorService extends EntityService<Credor> {
    /**
     * @summary Construtor da classe CredorService.
     * @description Chama o construtor da classe base com o endpoint específico
     */
    constructor() {
        super('credor');
    }
}
