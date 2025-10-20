import { EntityService } from '@/core/services/entity.service';
import { EntityDetailComponent } from '@/shared/components/entity-detail.component';
import { sharedConfig } from '@/shared/config/shared.config';
import { cpfValidator } from '@/shared/functions/cpf.validator';
import { Pagador } from '@/shared/models/pagador.model';
import { PagadorService } from '@/shared/services/pagador.service';
import { Component } from '@angular/core';
import { Validators } from '@angular/forms';
import { NgxMaskDirective } from 'ngx-mask';

@Component({
    selector: 'app-pagador-detail.component',
    imports: [...sharedConfig.imports, NgxMaskDirective],
    templateUrl: './pagador-detail.component.html',
    providers: [{provide: EntityService, useClass: PagadorService}],
})
export class PagadorDetailComponent extends EntityDetailComponent<Pagador> {
    constructor() {
        super(
            {
                nome: ['', [Validators.required, Validators.minLength(10)]],
                cpf: ['', [Validators.required, cpfValidator()]],
                email: ['', [Validators.required, Validators.email]],
            }
        );
    }
}
