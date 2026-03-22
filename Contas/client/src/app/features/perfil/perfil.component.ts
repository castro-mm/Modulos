import { Component, OnInit } from '@angular/core';
import { Perfil } from '@/shared/models/perfil.model';
import { PerfilParams } from '@/shared/params/perfil.params';
import { PerfilDetailComponent } from './detail/perfil-detail.component';
import { BreadcrumbComponent } from '@/core/components/breadcrumb.component';
import { sharedConfig } from '@/shared/config/shared.config';
import { PerfilService } from '@/shared/services/perfil.service';
import { EntityService } from '@/core/services/entity.service';
import { EntityListComponent } from '@/core/components/entity-list.component';
import { TableListComponent } from '@/core/components/table-list.component';
import { TableColumn } from '@/core/types/table-column.type';

@Component({
    selector: 'app-perfil',
    imports: [...sharedConfig.imports, BreadcrumbComponent, TableListComponent],
    templateUrl: './perfil.component.html',
    providers: [{ provide: EntityService, useClass: PerfilService }]
})
export class PerfilComponent extends EntityListComponent<Perfil, PerfilParams, PerfilDetailComponent> implements OnInit {
    columns: TableColumn[] = [
        { field: 'id', header: '#', type: 'number', width: '2rem', sortable: true },
        { field: 'name', header: 'Nome', type: 'text', width: '12rem', sortable: true },
        { field: 'criadoPor', header: 'Criado Por', type: 'text', width: '10rem', sortable: true },
        { field: 'dataDeCriacao', header: 'Data de Criação', type: 'date', format: 'dd/MM/yyyy HH:mm:ss', width: '10rem', sortable: true },
        { field: 'dataDeAtualizacao', header: 'Data de Atualização', type: 'date', format: 'dd/MM/yyyy HH:mm:ss', width: '10rem', sortable: true },
    ];

    filterFields = ['id', 'name', 'criadoPor'];

    constructor() {
        super({ name: [''] }, PerfilDetailComponent, '20%');
    }

    ngOnInit(): void { this.listar(); }
}
