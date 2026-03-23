import { Component } from '@angular/core';
import { Validators } from '@angular/forms';
import { Perfil } from '@/shared/models/perfil.model';
import { sharedConfig } from '@/shared/config/shared.config';
import { PerfilService } from '@/shared/services/perfil.service';
import { EntityDetailComponent } from '@/core/components/entity-detail.component';
import { EntityService } from '@/core/services/entity.service';
import { FieldValidationMessageComponent } from '@/core/components/field-validation-message.component';

@Component({
    selector: 'app-perfil-detail',
    imports: [...sharedConfig.imports, FieldValidationMessageComponent],
    templateUrl: './perfil-detail.component.html',
    providers: [{ provide: EntityService, useClass: PerfilService }]
})
export class PerfilDetailComponent extends EntityDetailComponent<Perfil> {
    fieldsLabels: { [key: string]: string } = { name: 'Nome' };

    constructor() {
        super(
            { name: ['', [Validators.required, Validators.minLength(3)]] },
        );
    }
}
