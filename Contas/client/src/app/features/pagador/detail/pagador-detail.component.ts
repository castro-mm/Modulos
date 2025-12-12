import { EntityService } from '@/core/services/entity.service';
import { EntityDetailComponent } from '@/core/components/entity-detail.component';
import { sharedConfig } from '@/shared/config/shared.config';
import { cpfValidator } from '@/shared/functions/cpf.validator';
import { Pagador } from '@/shared/models/pagador.model';
import { PagadorService } from '@/shared/services/pagador.service';
import { Component } from '@angular/core';
import { Validators } from '@angular/forms';
import { NgxMaskDirective } from 'ngx-mask';
import { FieldValidationMessageComponent } from "@/core/components/field-validation-message.component";

@Component({
    selector: 'app-pagador-detail.component',
    imports: [...sharedConfig.imports, NgxMaskDirective, FieldValidationMessageComponent],
    templateUrl: './pagador-detail.component.html',
    providers: [{ provide: EntityService, useClass: PagadorService }],
})
export class PagadorDetailComponent extends EntityDetailComponent<Pagador> {
    fieldsLabels: { [key: string]: string } = {
        nome: 'Nome',
        cpf: 'CPF',
        email: 'E-Mail'
    };

    constructor() {
        super(
            {
                nome: ['', [Validators.required, Validators.minLength(10)]],
                cpf: ['', [Validators.required, cpfValidator()]],
                email: ['', [Validators.required, Validators.email]],
            }
        );
        this.form.patchValue({ cpf: this.form.value.cpf?.toString().padStart(11, '0') || '' }, { emitEvent: false });
    }
}
