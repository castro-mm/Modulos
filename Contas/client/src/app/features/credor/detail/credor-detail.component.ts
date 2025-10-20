import { ApiResponse } from '@/core/models/api-response.model';
import { StatusCode } from '@/core/objects/enums';
import { EntityService } from '@/core/services/entity.service';
import { MessagesService } from '@/core/services/messages.service';
import { EntityDetailComponent } from '@/shared/components/entity-detail.component';
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
    imports: [...sharedConfig.imports, NgxMaskDirective],
    templateUrl: './credor-detail.component.html',
    providers: [{ provide: EntityService, useClass: CredorService }, provideNgxMask()],
})
export class CredorDetailComponent extends EntityDetailComponent<Credor> implements OnInit {
    segmentoDoCredorService = inject(SegmentoDoCredorService);
    messageService = inject(MessagesService);

    segmentoDoCredorOptions: { label: string; value: number }[] = [];

    constructor() {        
        super(
            {
                razaoSocial: ['', [Validators.required, Validators.minLength(10)]],
                nomeFantasia: ['', [Validators.required, Validators.minLength(3)]],
                cnpj: ['', [Validators.required, cnpjValidator()]],
                segmentoDoCredorId: [null, [Validators.required, Validators.min(1)]]
            }
        );        
    }

    async ngOnInit(): Promise<void> {
        const response: ApiResponse = await this.segmentoDoCredorService.getAll();

        if (response.statusCode === StatusCode.OK) {
            const items = response.data.items as SegmentoDoCredor[];
            this.segmentoDoCredorOptions = items.map(x => ({ label: x.nome, value: x.id }));
        } else {
            this.messageService.showMessageFromReponse((response as any).error);
        }
    }

    isInvalid(controlName: string): boolean {
        const control = this.form.get(controlName);
        return !!(control && control.invalid && (control.dirty || control.touched));
    }
}
