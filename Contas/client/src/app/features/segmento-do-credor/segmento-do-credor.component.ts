import { Component, OnInit, signal } from '@angular/core';
import { SegmentoDoCredor } from '../../shared/models/segmento-do-credor.model';
import { Table } from 'primeng/table';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { SegmentoDoCredorDetailComponent } from './detail/segmento-do-credor-detail.component';
import { MessagesService } from '@/core/services/messages.service';
import { ApiResponse } from '@/core/models/api-response.model';
import { BreadcrumbComponent } from "@/shared/components/breadcrumb.component";
import { FormBuilder, FormGroup } from '@angular/forms';
import { SegmentoDoCredorParams } from '../../shared/params/segmento-do-credor.params';
import { sharedConfig } from '@/shared/config/shared.config';
import { SegmentoDoCredorService } from '@/shared/services/segmento-do-credor.service';

@Component({
    selector: 'app-segmento-do-credor',
    imports: [...sharedConfig.imports, BreadcrumbComponent], 
    templateUrl: './segmento-do-credor.component.html',    
    providers: [SegmentoDoCredorService]
})
export class SegmentoDoCredorComponent implements OnInit {
    form: FormGroup;

    itens = signal<SegmentoDoCredor[]>([]); 
    itensSelecionados: SegmentoDoCredor[] | null = null;

    isLoading: boolean = false;
    dialogRef: DynamicDialogRef | null = null;

    constructor(
        private fb: FormBuilder,
        private segmentoDoCredorService: SegmentoDoCredorService, 
        private dialogService: DialogService, 
        private messageService: MessagesService,
    ) { 
        this.form = this.fb.group({ nome: [''] });        
    }    

    ngOnInit(): void {
        this.listar();        
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    async listar() {
        this.itens.set([]); 
        this.itensSelecionados = [];
        this.isLoading = true;

        this.form.patchValue({
            nome: (this.form.value as SegmentoDoCredorParams).nome === null 
                ? '' 
                : (this.form.value as SegmentoDoCredorParams).nome
        });

        try {
            const params = this.form.value as SegmentoDoCredorParams;        
            const response = await this.segmentoDoCredorService.getByParams(params);

            this.itens.set(response.data?.items as SegmentoDoCredor[]);
        } catch (error: any) {
            console.error('Erro ao listar segmentos do credor:', (error as ApiResponse));
            this.messageService.showMessageFromReponse(error.error as ApiResponse);
        }

        this.isLoading = false;
        this.form.reset();
    }

    async excluir(item: SegmentoDoCredor) {
        if (!item || !item.id) {
            this.messageService.showWarn('Item inválido ou não selecionado para exclusão.');
            return;
        }

        this.messageService.confirm({
            message: `Tem certeza que deseja excluir o segmento do credor "${item.nome}"?`,
            accept: async () => {
                this.isLoading = true;

                const response = await this.segmentoDoCredorService.delete(item.id!);
                if (response.statusCode === 200) {
                    this.itens.update(current => current.filter(i => i.id !== item.id));
                }
                this.messageService.showMessageFromReponse(response);

                this.isLoading = false; 
            },
            reject: () => { }
        });
    }

    async excluirItensSelecionados() {
        if (!this.itensSelecionados || this.itensSelecionados.length === 0) {
            this.messageService.showWarn('Nenhum item selecionado para exclusão.');
            return;
        }
        let ids = this.itensSelecionados?.map(i => i.id);
        this.messageService.confirm({
            message: `Tem certeza que deseja excluir os ${ids.length} registro(s) selecionado(s)?`,
            accept: async () => {
                this.isLoading = true;

                const response = await this.segmentoDoCredorService.deleteRange(ids as number[]);
                if (response.statusCode === 200) {
                    this.itens.update(current => current.filter(i => !ids?.includes(i.id!)));
                    this.itensSelecionados = [];    
                }
                this.messageService.showMessageFromReponse(response);

                this.isLoading = false;                        
            },
            reject: () => { 
                this.itensSelecionados = []; 
            }
        });
    }

    // THIS METHOD COULD BE PART OF A GENERIC BASE COMPONENT, OR A FUNCTION IN A UTILS SERVICE/HELPER
    openDialog(item: SegmentoDoCredor | null = null) {
        this.dialogRef = this.dialogService.open(SegmentoDoCredorDetailComponent, {
            header: `${item === null ? 'Novo': 'Edição'} - Segmento do Credor`,
            width: '20%',
            modal: true,
            data: {
                item: item
            }
        });

        this.dialogRef.onClose.subscribe(
            (response: ApiResponse) => {
                this.isLoading = true;
                if (response && response.statusCode === 200) {
                    this.messageService.showMessageFromReponse(response, item === null ? 'Registro criado com sucesso' : 'Registro atualizado com sucesso');
                    this.listar();

                    // este caso abaixo deve ser implementado apenas em funções que nao tem opção de filtro.
                    // this.itens.update(current => {
                    //     let returnValue: SegmentoDoCredor[] = [];
                    //     if (item) {
                    //         returnValue = current.map(i => i.id === item.id ? response.data as SegmentoDoCredor : i);
                    //     } else {                            
                    //         returnValue = [...current, response.data as SegmentoDoCredor];
                    //     }
                    //     this.messageService.showMessageFromReponse(
                    //         response, item === null 
                    //         ? 'Registro criado com sucesso' 
                    //         : 'Registro atualizado com sucesso'
                    //     );
                    //     return returnValue;
                    // });
                }
                this.isLoading = false;
            }
        )
    }    
}
