import { Component, OnInit } from '@angular/core';
import { SegmentoDoCredor } from '../../shared/models/segmento-do-credor.model';
import { SegmentoDoCredorDetailComponent } from './detail/segmento-do-credor-detail.component';
import { BreadcrumbComponent } from "@/shared/components/breadcrumb.component";
import { SegmentoDoCredorParams } from '../../shared/params/segmento-do-credor.params';
import { sharedConfig } from '@/shared/config/shared.config';
import { SegmentoDoCredorService } from '@/shared/services/segmento-do-credor.service';
import { EntityService } from '@/core/services/entity.service';
import { EntityListComponent } from '@/shared/components/entity-list.component';

@Component({
    selector: 'app-segmento-do-credor',
    imports: [...sharedConfig.imports, BreadcrumbComponent], 
    templateUrl: './segmento-do-credor.component.html',
    providers: [{ provide: EntityService, useClass: SegmentoDoCredorService }]
})
export class SegmentoDoCredorComponent extends EntityListComponent<SegmentoDoCredor, SegmentoDoCredorParams, SegmentoDoCredorDetailComponent> implements OnInit {
    constructor() {
        super({ nome: [''] }, SegmentoDoCredorDetailComponent, '20%');
    }

    ngOnInit(): void { this.listar(); }
}
