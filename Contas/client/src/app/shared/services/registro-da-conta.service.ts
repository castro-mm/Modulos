import { Injectable } from '@angular/core';
import { EntityService } from '@/core/services/entity.service';
import { RegistroDaConta } from '../models/registro-da-conta.model';

@Injectable({
    providedIn: 'root'
})
export class RegistroDaContaService extends EntityService<RegistroDaConta> {
    constructor() {
        super('registrodaconta');
    }
}
