import { Injectable } from '@angular/core';
import { SegmentoDoCredor } from '../models/segmento-do-credor.model';
import { EntityService } from '@/core/services/entity.service';

@Injectable({
    providedIn: 'root'
})
/**
 * @author Marcelo M. de Castro
 * @summary Serviço para gerenciar informações de segmentos do credor.
 * @description Este serviço permite a manipulação de dados relacionados a segmentos do credor.
 * @version 1.0.0
 */
export class SegmentoDoCredorService extends EntityService<SegmentoDoCredor> {
    /**
     * @summary Construtor da classe SegmentoDoCredorService.
     * @description Chama o construtor da classe base com o endpoint específico
     */
    constructor() {
        super('segmentodocredor');
    }
}
