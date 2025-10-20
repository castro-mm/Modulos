import { Component } from '@angular/core';
import { Validators } from '@angular/forms';
import { SegmentoDoCredor } from '../../../shared/models/segmento-do-credor.model';
import { sharedConfig } from '@/shared/config/shared.config';
import { SegmentoDoCredorService } from '@/shared/services/segmento-do-credor.service';
import { EntityDetailComponent } from '@/shared/components/entity-detail.component';
import { EntityService } from '@/core/services/entity.service';

@Component({
    selector: 'app-segmento-do-credor-detail.component',
    imports: [...sharedConfig.imports],
    templateUrl: './segmento-do-credor-detail.component.html',
    providers: [{ provide: EntityService, useClass: SegmentoDoCredorService }]
})
export class SegmentoDoCredorDetailComponent extends EntityDetailComponent<SegmentoDoCredor> {
    constructor() {
        super({
            nome: ['', [Validators.required, Validators.minLength(3)]]
        });
    }    
}