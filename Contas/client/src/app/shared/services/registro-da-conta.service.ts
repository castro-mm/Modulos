import { Injectable } from '@angular/core';
import { EntityService } from '@/core/services/entity.service';
import { RegistroDaConta } from '../models/registro-da-conta.model';

@Injectable({
    providedIn: 'root'
})
/**
 * @author Marcelo M. de Castro
 * @summary Serviço para gerenciar informações de registros de conta.
 * @description Este serviço permite a manipulação de dados relacionados a registros de conta.
 * @version 1.0.0
 */
export class RegistroDaContaService extends EntityService<RegistroDaConta> {
    /**
     * @summary Construtor da classe RegistroDaContaService.
     * @description Chama o construtor da classe base com o endpoint específico
     */
    constructor() {
        super('registrodaconta');
    }
}
