import { EntityListComponent } from '@/core/components/entity-list.component';
import { sharedConfig } from '@/shared/config/shared.config';
import { Credor } from '@/shared/models/credor.model';
import { CredorParams } from '@/shared/params/credor.params';
import { Component, inject, OnInit } from '@angular/core';
import { CredorDetailComponent } from './detail/credor-detail.component';
import { BreadcrumbComponent } from "@/core/components/breadcrumb.component";
import { CredorService } from '@/shared/services/credor.service';
import { EntityService } from '@/core/services/entity.service';
import { NgxMaskDirective } from 'ngx-mask';
import { cnpjValidator } from '@/shared/functions/cnpj.validator';
import { ApiResponse } from '@/core/types/api-response.type';
import { SegmentoDoCredorService } from '@/shared/services/segmento-do-credor.service';
import { StatusCode } from '@/core/objects/enums';
import { SegmentoDoCredor } from '@/shared/models/segmento-do-credor.model';
import { TableListComponent } from '@/core/components/table-list.component';
import { TableColumn } from '@/core/types/table-column.type';
import { BadgeModule } from 'primeng/badge';

@Component({
    selector: 'app-credor.component',
    imports: [...sharedConfig.imports, BadgeModule, TableListComponent, BreadcrumbComponent, NgxMaskDirective],
    templateUrl: './credor.component.html',
    providers: [{ provide: EntityService, useClass: CredorService }]
})
export class CredorComponent extends EntityListComponent<Credor, CredorParams, CredorDetailComponent> implements OnInit {
    segmentoDoCredorService = inject(SegmentoDoCredorService);
    segmentoDoCredorOptions: { label: string; value: number }[] = [];    

    columns: TableColumn[] = [
        { field: 'id', header: '#', width: '2rem', sortable: true },
        { field: 'razaoSocial', header: 'Razão Social', width: '16rem', sortable: true },
        { field: 'nomeFantasia', header: 'Nome Fantasia', width: '16rem', sortable: true },
        { field: 'cnpj', header: 'CNPJ', width: '14rem', pipe: 'cnpj', sortable: true },
        { field: 'segmentoDoCredor.nome', header: 'Segmento do Credor', width: '14rem', align: 'center', sortable: true },
        { field: 'dataDeCriacao', header: 'Data de Criação', width: '8rem', type: 'date', format: 'dd/MM/yyyy HH:mm:ss', sortable: true },
        { field: 'dataDeAtualizacao', header: 'Data de Atualização', width: '8rem', type: 'date', format: 'dd/MM/yyyy HH:mm:ss', sortable: true },
    ];

    filterFields = ['id', 'razaoSocial', 'nomeFantasia', 'cnpj'];

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
            const items = response.result?.data.items as SegmentoDoCredor[];
            this.segmentoDoCredorOptions = items.map(x => ({ label: x.nome, value: x.id }));
        } else {
            this.messageService.showMessageFromReponse((response as any).error);
        }
    }
}
