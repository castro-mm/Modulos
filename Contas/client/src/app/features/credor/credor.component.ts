import { EntityListComponent } from '@/shared/components/entity-list.component';
import { sharedConfig } from '@/shared/config/shared.config';
import { Credor } from '@/shared/models/credor.model';
import { CredorParams } from '@/shared/params/credor.params';
import { Component, inject, OnInit } from '@angular/core';
import { CredorDetailComponent } from './detail/credor-detail.component';
import { CnpjPipe } from "../../shared/pipes/cnpj.pipe";
import { BreadcrumbComponent } from "@/shared/components/breadcrumb.component";
import { CredorService } from '@/shared/services/credor.service';
import { EntityService } from '@/core/services/entity.service';
import { NgxMaskDirective } from 'ngx-mask';
import { cnpjValidator } from '@/shared/functions/cnpj.validator';
import { ApiResponse } from '@/core/models/api-response.model';
import { SegmentoDoCredorService } from '@/shared/services/segmento-do-credor.service';
import { StatusCode } from '@/core/objects/enums';
import { SegmentoDoCredor } from '@/shared/models/segmento-do-credor.model';

@Component({
    selector: 'app-credor.component',
    imports: [...sharedConfig.imports, CnpjPipe, BreadcrumbComponent, NgxMaskDirective],
    templateUrl: './credor.component.html',
    providers: [{ provide: EntityService, useClass: CredorService }]
})
export class CredorComponent extends EntityListComponent<Credor, CredorParams, CredorDetailComponent> implements OnInit {
    segmentoDoCredorService = inject(SegmentoDoCredorService);
    segmentoDoCredorOptions: { label: string; value: number }[] = [];

    constructor() {
        super(
            {
                razaoSocial: [''],
                nomeFantasia: [''],
                cnpj: ['', cnpjValidator()],
                segmentoDoCredorId: [null]
            },
            CredorDetailComponent,
            '30%'
        );
    }
    // TODO: Não carregar a lista ao iniciar em produção
    ngOnInit(): void { 
        this.carregaSegmentoDoCredorOptions();
        this.listar(); 
    }

    async carregaSegmentoDoCredorOptions() {
        const response: ApiResponse = await this.segmentoDoCredorService.getAll();

        if (response.statusCode === StatusCode.OK) {
            const items = response.data.items as SegmentoDoCredor[];
            this.segmentoDoCredorOptions = items.map(x => ({ label: x.nome, value: x.id }));
        } else {
            this.messageService.showMessageFromReponse((response as any).error);
        }
    }
}
