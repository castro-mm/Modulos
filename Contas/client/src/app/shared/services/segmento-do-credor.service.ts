import { Injectable } from '@angular/core';
import { SegmentoDoCredorParams } from '../params/segmento-do-credor.params';
import { SegmentoDoCredor } from '../models/segmento-do-credor.model';
import { EntityService } from '@/core/services/entity.service';

@Injectable({
    providedIn: 'root'
})
export class SegmentoDoCredorService extends EntityService<SegmentoDoCredor> {
    constructor() {
        super('segmentodocredor');
    }
}
