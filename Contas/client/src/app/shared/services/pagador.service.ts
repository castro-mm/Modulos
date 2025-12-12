import { EntityService } from "@/core/services/entity.service";
import { Injectable } from "@angular/core";
import { Pagador } from "../models/pagador.model";

@Injectable({
    providedIn: 'root'
})
/**
 * @author Marcelo M. de Castro
 * @summary Serviço para gerenciar informações de pagadores.
 * @description Este serviço permite a manipulação de dados relacionados a pagadores.
 * @version 1.0.0
 */
export class PagadorService extends EntityService<Pagador> {
    /**
     * @summary Construtor da classe PagadorService.
     * @description Chama o construtor da classe base com o endpoint específico
     */
    constructor() {
        super('pagador');
    }
}