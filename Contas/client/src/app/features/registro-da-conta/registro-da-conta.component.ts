import { Component, inject, OnInit } from '@angular/core';
import { RegistroDaContaDetailComponent } from './detail/registro-da-conta-detail.component';
import { BreadcrumbComponent } from "@/shared/components/breadcrumb.component";
import { sharedConfig } from '@/shared/config/shared.config';
import { EntityService } from '@/core/services/entity.service';
import { EntityListComponent } from '@/shared/components/entity-list.component';
import { RegistroDaContaService } from '@/shared/services/registro-da-conta.service';
import { RegistroDaConta } from '@/shared/models/registro-da-conta.model';
import { RegistroDaContaParams } from '@/shared/params/registro-da-conta.params';
import { CommonService } from '@/core/services/common.service';
import { KeyValuePair } from '@/core/models/key-value-pair.model';
import { Validators } from '@angular/forms';
import { CredorService } from '@/shared/services/credor.service';
import { Credor } from '@/shared/models/credor.model';
import { StatusCode, StatusDaConta } from '@/core/objects/enums';
import { PagadorService } from '@/shared/services/pagador.service';
import { Pagador } from '@/shared/models/pagador.model';
import { MesPipe } from '@/shared/pipes/mes.pipe';
import { StatusDaContaPipe } from '@/shared/pipes/status-da-conta.pipe';
import { Tag } from 'primeng/tag';

@Component({
    selector: 'app-registro-da-conta',
    imports: [...sharedConfig.imports, BreadcrumbComponent, MesPipe, StatusDaContaPipe, Tag], 
    templateUrl: './registro-da-conta.component.html',
    providers: [{ provide: EntityService, useClass: RegistroDaContaService }]
})
export class RegistroDaContaComponent extends EntityListComponent<RegistroDaConta, RegistroDaContaParams, RegistroDaContaDetailComponent> implements OnInit {
    commonService = inject(CommonService);
    credorService = inject(CredorService);
    pagadorService = inject(PagadorService);
    
    mesOptions: KeyValuePair[] = [];
    anoOptions: KeyValuePair[] = [];
    credorOptions: KeyValuePair[] = [];
    pagadorOptions: KeyValuePair[] = [];
    statusDaContaOptions: KeyValuePair[] = [];
    
    constructor() {
        super(
            { 
                mes: [new Date().getMonth(), [Validators.required]], 
                ano: [new Date().getFullYear(), [Validators.required]],
                credorId: [null], 
                pagadorId: [null],
                statusDaConta: [null] 
            }, 
            RegistroDaContaDetailComponent, 
            '60%'
        );
    }

    ngOnInit(): void { 
        this.mesOptions = this.commonService.getMonthsList();
        this.anoOptions = this.commonService.getYearsList(2025);   
        this.statusDaContaOptions = this.commonService.getStatusDaContaOptions();

        this.carregarListaDeCredores(); 
        this.carregarListaDePagadores(); 

        this.listar();
    }

    carregarListaDeCredores() {
        this.credorService.getAll().then(response => {
            if (response.statusCode === StatusCode.OK) {
                const items = response.data.items as Credor[];
                this.credorOptions = items.map((x: Credor) => ({ key: x.id, value: x.nomeFantasia }));
            } else {
                this.messageService.showMessageFromReponse((response as any).error);
            }
        });
    }

    carregarListaDePagadores() {
        this.pagadorService.getAll().then(response => {
            if (response.statusCode === StatusCode.OK) {
                const items = response.data.items as Pagador[];
                this.pagadorOptions = items.map((x: Pagador) => ({ key: x.id, value: x.nome }));
            } else {
                this.messageService.showMessageFromReponse((response as any).error);
            }
        });
    }

    getStatusSeverity(status: number): string {
        switch (status) {
          case 0:
            return 'warn';      // Amarelo/laranja
          case 1: 
            return 'success';      // Verde
          case 2:
            return 'danger';       // Vermelho
          case 3:
            return 'secondary';    // Cinza
          default:
            return 'secondary';    // Cor padrão (cinza)
        }
    }
}
