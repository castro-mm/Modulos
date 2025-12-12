import { BreadcrumbComponent } from '@/core/components/breadcrumb.component';
import { EntityListComponent } from '@/core/components/entity-list.component';
import { sharedConfig } from '@/shared/config/shared.config';
import { cpfValidator } from '@/shared/functions/cpf.validator';
import { Pagador } from '@/shared/models/pagador.model';
import { PagadorParams } from '@/shared/params/pagador.params';
import { CpfPipe } from '@/shared/pipes/cpf.pipe';
import { Component, OnInit } from '@angular/core';
import { NgxMaskDirective } from 'ngx-mask';
import { PagadorDetailComponent } from './detail/pagador-detail.component';
import { EntityService } from '@/core/services/entity.service';
import { PagadorService } from '@/shared/services/pagador.service';
import { TableListComponent } from '@/core/components/table-list.component';
import { TableColumn } from '@/core/types/table-column.type';

@Component({
    selector: 'app-pagador.component',
    imports: [...sharedConfig.imports, TableListComponent, BreadcrumbComponent, NgxMaskDirective],
    templateUrl: './pagador.component.html',
    providers: [{ provide: EntityService, useClass: PagadorService }]
})
export class PagadorComponent extends EntityListComponent<Pagador, PagadorParams, PagadorDetailComponent> implements OnInit {    
    columns: TableColumn[] = [
        { field: 'id', header: '#', width: '2rem', sortable: true },
        { field: 'nome', header: 'Nome', width: '16rem', sortable: true },
        { field: 'email', header: 'Email', width: '16rem', sortable: true },
        { field: 'cpf', header: 'Cpf', width: '14rem', pipe: 'cpf', sortable: true },
        { field: 'dataDeCriacao', header: 'Data de Criação', width: '8rem', type: 'date', format: 'dd/MM/yyyy HH:mm:ss', sortable: true },
        { field: 'dataDeAtualizacao', header: 'Data de Atualização', width: '8rem', type: 'date', format: 'dd/MM/yyyy HH:mm:ss', sortable: true }
    ];

    constructor() {
        super(
            {
                nome: [''],
                cpf: ['', cpfValidator()],
            },
            PagadorDetailComponent,
            '30%'
        );
    }

    // TODO: Não carregar a lista ao iniciar em produção
    ngOnInit(): void {
        this.listar(); 
    }
}
