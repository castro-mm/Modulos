import { BreadcrumbComponent } from '@/shared/components/breadcrumb.component';
import { EntityListComponent } from '@/shared/components/entity-list.component';
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

@Component({
    selector: 'app-pagador.component',
    imports: [...sharedConfig.imports, CpfPipe, BreadcrumbComponent, NgxMaskDirective],
    templateUrl: './pagador.component.html',
    providers: [{ provide: EntityService, useClass: PagadorService }]
})
export class PagadorComponent extends EntityListComponent<Pagador, PagadorParams, PagadorDetailComponent> implements OnInit {    
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
