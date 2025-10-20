import { ApiResponse } from "@/core/models/api-response.model";
import { Entity } from "@/core/models/entity.model";
import { Params } from "@/core/models/params.model";
import { StatusCode } from "@/core/objects/enums";
import { EntityService } from "@/core/services/entity.service";
import { MessagesService } from "@/core/services/messages.service";
import { ModalService } from "@/core/services/modal.service";
import { Component, inject, signal, Type } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";

/**
 * @author Marcelo M. de Castro
 * @summary Componente base para listagem e gerenciamento de entidades.
 * Fornece funcionalidades comuns como listagem, filtro, seleção, exclusão e abertura de diálogos para criação/edição.
 * @template T Tipo da entidade que o componente gerencia.
 * @template TParams Tipo dos parâmetros de filtro utilizados na listagem.
 * @template TComponent Tipo do componente de detalhe utilizado para criação/edição de entidades.
 * @example
 * ```typescript
 * import { Component, OnInit } from '@angular/core';
 * import { signal } from '@angular/core';
 * import { SegmentoDoCredor } from '../../shared/models/segmento-do-credor.model';
 * import { SegmentoDoCredorDetailComponent } from './detail/segmento-do-credor-detail.component';
 * import { BreadcrumbComponent } from "@/shared/components/breadcrumb.component";
 * import { SegmentoDoCredorParams } from '../../shared/params/segmento-do-credor.params';
 * import { sharedConfig } from '@/shared/config/shared.config';
 * import { SegmentoDoCredorService } from '@/shared/services/segmento-do-credor.service';
 * import { EntityService } from '@/core/services/entity.service';
 * import { EntityListComponent } from '@/shared/components/entity-list.component';
 * 
 * @Component({
 *     selector: 'app-segmento-do-credor',
 *     imports: [...sharedConfig.imports, BreadcrumbComponent], 
 *     templateUrl: './segmento-do-credor.component.html',
 *     providers: [{ provide: EntityService, useClass: SegmentoDoCredorService }]
 * })
 * export class SegmentoDoCredorComponent extends EntityListComponent<SegmentoDoCredor, SegmentoDoCredorParams, SegmentoDoCredorDetailComponent> implements OnInit {
 *     constructor() {
 *         super({ nome: [''] }, SegmentoDoCredorDetailComponent);
 *     }
 * 
 *     ngOnInit(): void { this.listar(); } // Opcional: Chama a listagem ao inicializar o componente
 * }
 * ```
 */
export abstract class EntityListComponent<T extends Entity, TParams extends Params, TComponent> {
    form: FormGroup;

    entityList = signal<T[]>([]);
    itensSelecionados: T[] | null = null;

    isLoading: boolean = false;
    modalSize: string = '20%'; 
    
    fb = inject(FormBuilder);
    modalService = inject(ModalService);
    messageService = inject(MessagesService); 
    service: EntityService<T> = inject(EntityService);

    /**
     * @param modalSize Define a largura do modal que será aberto para criação/edição de entidades.
     * @param initialFormValues Objeto contendo os valores iniciais do formulário. 
     * @param detailComponent Tipo do componente de detalhe utilizado para criação/edição de entidades.
     */
    constructor(initialFormValues: { [key: string]: any }, protected detailComponent: Type<TComponent>, modalSize: string) {
        this.modalSize = modalSize;
        this.form = this.fb.group(initialFormValues);
    }

    /**
     * @summary Lista as entidades com base nos parâmetros do formulário.
     * @description Atualiza a lista de entidades com os resultados da consulta.
     * Gerencia o estado de carregamento e exibe mensagens de erro, se necessário.
     * @returns Promise<void>
     */
    async listar(): Promise<void>{
        this.entityList.set([]); 
        this.itensSelecionados = [];
        this.isLoading = true;

        this.form.patchValue(
            Object.keys(this.form.value).reduce(
                (acc, key) => {
                    acc[key] = (this.form.value as any)[key] === null ? '' : (this.form.value as any)[key];
                    return acc;
                }, 
                {} as { [key: string]: TParams }
            )
        );
        
        try {
            const params = this.form.value as TParams;//{ [key: string]: any };
            const response: ApiResponse = await (this as any).service.getByParams(params); // maybe just get()

            if (response.statusCode === StatusCode.OK) {
                this.entityList.set(response.data.items as T[]);
            } else {
                this.messageService.showMessageFromReponse(response);
            }
        } catch (err: any) {
            console.error('Error ao listar os itens:', err as ApiResponse);
            this.messageService.showMessageFromReponse(err.error as ApiResponse);
        } finally {
            this.isLoading = false;
        }
    }
    /**
     * @summary Aplica um filtro global na tabela.
     * @param table Referência à tabela do primeng onde o filtro será aplicado.
     * @param event Evento de entrada do filtro.
     */
    onGlobalFilter(table: any, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    /**
     * @summary Exclui uma entidade específica.
     * @param entity Entidade a ser excluída.
     * @description Exibe uma confirmação antes de proceder com a exclusão.
     * Atualiza a lista de entidades após a exclusão e exibe mensagens de sucesso ou erro.
     */
    excluir(entity: T): void {
        this.messageService.confirm({
            header: 'Confirmação',
            message: 'Tem certeza que deseja excluir este registro?',
            accept: async () => {
                if (!entity || !entity.id) {
                    this.messageService.showWarn('Item inválido ou não selecionado para exclusão.');
                    return;
                }

                this.isLoading = true;

                const response = await this.service.delete(entity.id);
                if (response.statusCode === StatusCode.OK) {
                    //this.entityList.update(current => current.filter(i => i.id !== entity.id)); Usar este codigo apenas quando a listagem nao precisar ser atualizada da api.
                    await this.listar();
                } 
                this.messageService.showMessageFromReponse(response);
                this.isLoading = false;
            }
        })
    }

    /**
     * @summary Exclui vários itens selecionadas.
     * @description Verifica se há itens selecionados, exibe uma confirmação antes de proceder com a exclusão.
     * Atualiza a lista de entidades após a exclusão e exibe mensagens de sucesso ou erro.
     */
    async excluirVarios() {
        if (!this.itensSelecionados || this.itensSelecionados.length === 0) {
            this.messageService.showWarn('Nenhum item selecionado para exclusão.');
            return;
        }

        const ids = this.itensSelecionados.map(i => i.id).filter(id => id !== undefined) as number[];

        this.messageService.confirm({
            header: 'Confirmação',
            message: `Tem certeza que deseja excluir os ${ids.length} registro(s) selecionado(s)?`,
            accept: async () => {
                this.isLoading = true;

                const response = await this.service.deleteRange(ids);
                if (response.statusCode === StatusCode.OK) {
                    //this.entityList.update(current => current.filter(i => !ids.includes(i.id!))); Usar este codigo apenas quando a listagem nao precisa ser atualizada da api.
                    await this.listar();
                } 
                this.messageService.showMessageFromReponse(response);
                this.isLoading = false;
            },
            reject: () => { this.itensSelecionados = []; }
        });
    }

    /**
     * @summary Abre um dialog para criação ou edição de uma entidade.
     * @param entity Entidade a ser editada. Se nula, o diálog será aberto para criação de uma nova entidade.
     * @description Após o fechamento do diálog, atualiza a lista de entidades se houver alterações.
     * Exibe mensagens de sucesso ou erro com base na resposta da API.
     */
    openDialog(entity: T | null = null) {
        const onClose = this.modalService.openDialogPage(
            this.detailComponent as Type<Component>, 
            entity ? 'Editar Registro' : 'Novo Registro',
            { item: entity },
            this.modalSize
        );

        onClose.subscribe(
            async (response: ApiResponse) => {
                this.isLoading = true;
                if (response && response.statusCode === StatusCode.OK) {
                    this.messageService.showMessageFromReponse(
                        response, 
                        entity === null ? 'Registro criado com sucesso' : 'Registro atualizado com sucesso'
                    );
                    await this.listar();                    
                }
                this.isLoading = false;
            }
        );
    }
}