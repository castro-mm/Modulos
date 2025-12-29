import { ApiResponse } from '@/core/types/api-response.type';
import { SelectOption } from '@/core/types/select-option.type';
import { StatusCode } from '@/core/objects/enums';
import { EntityService } from '@/core/services/entity.service';
import { MessagesService } from '@/core/services/messages.service';
import { EntityDetailComponent } from '@/core/components/entity-detail.component';
import { FieldValidationMessageComponent } from '@/core/components/field-validation-message.component';
import { sharedConfig } from '@/shared/config/shared.config';
import { cnpjValidator } from '@/shared/functions/cnpj.validator';
import { Credor } from '@/shared/models/credor.model';
import { SegmentoDoCredor } from '@/shared/models/segmento-do-credor.model';
import { CredorService } from '@/shared/services/credor.service';
import { SegmentoDoCredorService } from '@/shared/services/segmento-do-credor.service';
import { Component, inject, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';

@Component({
    selector: 'app-credor-detail.component',
    imports: [...sharedConfig.imports, NgxMaskDirective, FieldValidationMessageComponent],
    templateUrl: './credor-detail.component.html',
    providers: [{ provide: EntityService, useClass: CredorService }, provideNgxMask()],
})
export class CredorDetailComponent extends EntityDetailComponent<Credor> implements OnInit {
    segmentoDoCredorService = inject(SegmentoDoCredorService);

    segmentoDoCredorOptions: SelectOption[] = [];

    fieldsLabels: { [key: string]: string } = {
        razaoSocial: 'Razão Social',
        nomeFantasia: 'Nome Fantasia',
        cnpj: 'CNPJ',
        segmentoDoCredorId: 'Segmento do Credor'
    };

    constructor() {        
        super(
            {
                razaoSocial: ['', [Validators.required, Validators.minLength(10)]],
                nomeFantasia: ['', [Validators.required, Validators.minLength(3)]],
                cnpj: ['', [Validators.required, cnpjValidator()]],
                segmentoDoCredorId: [null, [Validators.required, Validators.min(1)]]
            }
        );   
        this.form.patchValue({ cnpj: this.form.value.cnpj?.toString().padStart(14, '0') || '' }, { emitEvent: false });
    }

    async ngOnInit(): Promise<void> {
        const response: ApiResponse = await this.segmentoDoCredorService.getAll();

        if (response.statusCode === StatusCode.OK) {
            const items = response.result?.data.items as SegmentoDoCredor[];
            this.segmentoDoCredorOptions = items.map(x => ({ label: x.nome, value: x.id, icon: '' }));
        } else {
            this.messageService.showMessageFromReponse((response as any).error);
        }
    }    
}
