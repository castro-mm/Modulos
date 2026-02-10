import { Component, OnInit } from '@angular/core';
import { SegmentoDoCredor } from '../../shared/models/segmento-do-credor.model';
import { SegmentoDoCredorDetailComponent } from './detail/segmento-do-credor-detail.component';
import { BreadcrumbComponent } from "@/core/components/breadcrumb.component";
import { SegmentoDoCredorParams } from '../../shared/params/segmento-do-credor.params';
import { sharedConfig } from '@/shared/config/shared.config';
import { SegmentoDoCredorService } from '@/shared/services/segmento-do-credor.service';
import { EntityService } from '@/core/services/entity.service';
import { EntityListComponent } from '@/core/components/entity-list.component';
import { TableListComponent } from '@/core/components/table-list.component';
import { TableColumn } from '@/core/types/table-column.type';

@Component({
    selector: 'app-segmento-do-credor',
    imports: [...sharedConfig.imports, BreadcrumbComponent, TableListComponent], 
    templateUrl: './segmento-do-credor.component.html',
    providers: [{ provide: EntityService, useClass: SegmentoDoCredorService }]
})
export class SegmentoDoCredorComponent extends EntityListComponent<SegmentoDoCredor, SegmentoDoCredorParams, SegmentoDoCredorDetailComponent> implements OnInit {
    columns: TableColumn[] = [
        { field: 'id', header: '#', type: 'number', width: '2rem', sortable: true },
        { field: 'nome', header: 'Nome', type: 'text', width: '20rem', sortable: true },
        { field: 'dataDeCriacao', header: 'Data de Criação', type: 'date', format: 'dd/MM/yyyy HH:mm:ss', width: '12rem', sortable: true },
        { field: 'dataDeAtualizacao', header: 'Data de Atualização', type: 'date', format: 'dd/MM/yyyy HH:mm:ss', width: '12rem', sortable: true },
    ];

    filterFields = ['id', 'nome'];

    constructor() {
        super({ nome: [''] }, SegmentoDoCredorDetailComponent, '20%');
    }

    ngOnInit(): void { this.listar(); }
}
