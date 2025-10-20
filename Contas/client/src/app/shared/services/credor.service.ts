import { EntityService } from '@/core/services/entity.service';
import { Injectable } from '@angular/core';
import { Credor } from '../models/credor.model';

@Injectable({
    providedIn: 'root'
})
export class CredorService extends EntityService<Credor> {
    constructor() {
        super('credor');
    }
}
