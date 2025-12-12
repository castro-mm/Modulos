import { Component } from '@angular/core';
import { Validators } from '@angular/forms';
import { SegmentoDoCredor } from '../../../shared/models/segmento-do-credor.model';
import { sharedConfig } from '@/shared/config/shared.config';
import { SegmentoDoCredorService } from '@/shared/services/segmento-do-credor.service';
import { EntityDetailComponent } from '@/core/components/entity-detail.component';
import { EntityService } from '@/core/services/entity.service';
import { FieldValidationMessageComponent } from "@/core/components/field-validation-message.component";

@Component({
    selector: 'app-segmento-do-credor-detail.component',
    imports: [...sharedConfig.imports, FieldValidationMessageComponent],
    templateUrl: './segmento-do-credor-detail.component.html',
    providers: [{ provide: EntityService, useClass: SegmentoDoCredorService }]
})
export class SegmentoDoCredorDetailComponent extends EntityDetailComponent<SegmentoDoCredor> {
    fieldsLabels: {[key: string]: string} = { nome: 'Nome' };

    constructor() {
        super({ nome: ['', [Validators.required, Validators.minLength(6)]] });
    }    
}