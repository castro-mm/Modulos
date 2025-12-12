import { Component, inject, input, output, TemplateRef, Type } from "@angular/core";
import { sharedConfig } from "../../shared/config/shared.config";
import { Entity } from "@/core/models/entity.model";
import { ModalService } from "@/core/services/modal.service";
import { MessagesService } from "@/core/services/messages.service";
import { ApiResponse } from "@/core/types/api-response.type";
import { StatusCode } from "@/core/objects/enums";
import { TableColumn } from "@/core/types/table-column.type";
import { CustomFormatPipe } from "../../shared/pipes/custom-format.pipe";
import { NestedFieldPipe } from "../../shared/pipes/nested-field.pipe";
import { DynamicFormatPipe } from "../../shared/pipes/dynamic-format.pipe";

@Component({
    selector: 'app-table-list',
    imports: [...sharedConfig.imports, CustomFormatPipe, NestedFieldPipe, DynamicFormatPipe],
    template: `
        <p-table        
            #dt
            [value]="this.entityList()"
            [columns]="cols()"
            [rows]="rows()"
            [paginator]="true"
            [globalFilterFields]="globalFilterFields()"
            [(selection)]="itensSelecionados"
            [rowHover]="true"
            dataKey="id"
            currentPageReportTemplate="{first} até {last} de {totalRecords} registros"
            [showCurrentPageReport]="true"
            [rowsPerPageOptions]="[10, 20, 30]"
            size="small"
            [loading]="isLoading()"
            [loadingIcon]="'pi pi-spinner pi-spin'"
        >
            <ng-template #caption>
                <div class="flex items-center justify-between">
                    <div class="m-0">
                        <p-button pTooltip="Novo registro" icon="pi pi-plus" (click)="openDialog()" class="mr-2"></p-button>
                        <p-button pTooltip="Excluir itens selcionados" severity="danger" icon="pi pi-trash" (click)="excluirVarios()" [disabled]="!itensSelecionados || !itensSelecionados.length" />        
                    </div>
                    <p-iconfield>
                        <p-inputicon class="pi pi-search" />
                        <input pInputText type="text" (input)="onGlobalFilter(dt, $event)" placeholder="Pesquisar..." />
                    </p-iconfield>  
                </div>
            </ng-template>
            <ng-template #header let-columns>
                <tr>
                    <th style="width: 3rem; background-color: #f8f9fa">
                        <p-tableHeaderCheckbox />
                    </th>
                    @for(col of columns; track col) {
                        <th [style.min-width]="col.width || 'auto'" [style.text-align]="col.align || 'left'" [pSortableColumn]="col.sortable ? col.field : null" style="background-color: #f8f9fa">
                            {{ col.header }}
                            @if (col.sortable) {
                                <p-sortIcon [field]="col.field" />
                            }
                        </th>
                    }                    
                    <th style="min-width: 3rem; background-color: #f8f9fa"></th>
                </tr>
            </ng-template>
            <ng-template #body let-item let-columns="columns">
                <tr>
                    <td style="width: 3rem">
                        <p-tableCheckbox [value]="item" />
                    </td>
                    @for(col of columns; track col) {
                        <td [style.min-width]="col.width || 'auto'" [style.text-align]="col.align || 'left'">
                            @if (col.template && templates()[col.template]) {
                                <!-- Usa template customizado -->
                                <ng-container *ngTemplateOutlet="templates()[col.template]; context: { $implicit: item }"></ng-container>
                            }
                            @else if (col.pipe) {
                                <!-- Usa pipe customizado (cnpj, cpf, etc) -->
                                {{ item | nestedField: col.field | customFormat: col }}
                            } @else if (col.type) { 
                                <!-- Usa formatação por tipo (date, currency, etc) -->
                                 {{ item | nestedField: col.field | dynamicFormat: col }}
                            } @else {
                                <!-- Texto simples -->
                                {{ item | nestedField: col.field }}
                            }
                        </td>
                    }
                    <td style="text-align: right">    
                        <p-button icon="pi pi-pencil" class="mr-2" outlined (click)="openDialog(item)" pTooltip="Editar" />
                        <p-button icon="pi pi-trash" severity="danger" outlined (click)="this.onExcluir.emit(item)" pTooltip="Excluir" />
                    </td>
                </tr>
            </ng-template>
        </p-table>    
    `
})
/**
 * @author Marcelo M. de Castro
 * @summary Componente genérico para exibição de listas em tabela com funcionalidades de paginação, filtro, seleção e ações CRUD.
 * @template T Tipo genérico que estende a interface Entity, representando o tipo de dados exibidos na tabela.
 * @version 1.0.0
 */
export class TableListComponent<T extends Entity> {
    protected modalService = inject(ModalService);
    protected messageService = inject(MessagesService); 

    globalFilterFields = input.required<string[]>();
    rows = input<number>(10);
    cols = input.required<TableColumn[]>();
    entityList = input.required<T[]>();
    modalSize = input<string>('50%');
    detailComponent = input.required<Type<any>>();
    isLoading = input<boolean>(false);    
    templates = input<{ [key: string]: TemplateRef<any> }>({});

    onListar = output();
    onExcluir = output<T>();
    onExcluirVarios = output<number[]>();

    itensSelecionados: T[] | null = null;    

    constructor() { }
   
    /**
     * @summary Aplica um filtro global na tabela.
     * @param table Referência à tabela do primeng onde o filtro será aplicado.
     * @param event Evento de entrada do filtro.
     */
    onGlobalFilter(table: any, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    /**
     * @summary Abre um dialog para criação ou edição de uma entidade.
     * @param entity Entidade a ser editada. Se nula, o diálog será aberto para criação de uma nova entidade.
     * @description Após o fechamento do diálog, atualiza a lista de entidades se houver alterações.
     * Exibe mensagens de sucesso ou erro com base na resposta da API.
     */
    openDialog(entity: T | null = null) {
        const onClose = this.modalService.openDialogPage(
            this.detailComponent(), 
            entity ? 'Editar Registro' : 'Novo Registro',
            { item: entity },
            this.modalSize(),
        );

        onClose.subscribe(
            async (response: ApiResponse) => {
                if (response && response.statusCode === StatusCode.OK) {
                    this.messageService.showMessageFromReponse(
                        response, 
                        entity === null ? 'Registro criado com sucesso.' : 'Registro atualizado com sucesso.'
                    );
                }
                this.onListar.emit();
            }
        );
    }

    /**
     * @summary Exclui vários itens selecionadas.
     * @description Verifica se há itens selecionados, exibe uma confirmação antes de proceder com a exclusão.
     * Atualiza a lista de entidades após a exclusão e exibe mensagens de sucesso ou erro.
     */
    excluirVarios() {
        if (!this.itensSelecionados || this.itensSelecionados.length === 0) {
            this.messageService.showWarn('Nenhum item selecionado para exclusão.');
            return;
        }

        const ids = this.itensSelecionados.map(i => i.id).filter(id => id !== undefined) as number[];
        this.onExcluirVarios.emit(ids);
        this.itensSelecionados = [];
    }
}

