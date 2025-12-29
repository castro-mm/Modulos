import { ApiResponse } from "@/core/types/api-response.type";
import { Entity } from "@/core/models/entity.model";
import { EntityService } from "@/core/services/entity.service";
import { inject, signal } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { DynamicDialogConfig, DynamicDialogRef } from "primeng/dynamicdialog";
import { FieldValidationService } from "../services/field-validation.service";
import { CommonService } from "@/core/services/common.service";
import { EntityDetailMode } from "../types/entity-detail-mode.type";
import { MessagesService } from "../services/messages.service";
import { StatusCode } from "../objects/enums";

/**
 * @author Marcelo M. de Castro
 * @summary Componente base para criação e edição de entidades.
 * Fornece funcionalidades comuns como gerenciamento de formulário, carregamento de dados e salvamento.
 * @template T Tipo da entidade que o componente gerencia.
 * @example
 * ```typescript
 * import { Component } from '@angular/core';
 * import { Validators } from '@angular/forms';
 * import { MyEntity } from '@/shared/models/my-entity.model';
 * import { sharedConfig } from '@/shared/config/shared.config';
 * import { MyEntityService } from '@/shared/services/my-entity.service';
 * import { EntityComponent } from '@/shared/components/entity.component';
 * import { EntityService } from '@/core/services/entity.service';
 * 
 * @Component({
 *     imports: [...sharedConfig.imports],
 *     templateUrl: './my-entity-detail.component.html',
 *     providers: [{ provide: EntityService, useClass: MyEntityService }]
 * })
 * export class MyEntityDetailComponent extends EntityComponent<MyEntity> {
 *     constructor() {
 *         super({
 *             name: ['', [Validators.required, Validators.minLength(3)]]
 *         });
 *     }    
 * }
 * ```
 */
export abstract class EntityDetailComponent<T extends Entity> {
    protected fb = inject(FormBuilder); 
    protected service = inject(EntityService);
    protected activatedRoute = inject(ActivatedRoute);
    protected dialogRef = inject(DynamicDialogRef);
    protected dialogConfig = inject(DynamicDialogConfig);
    protected validationService = inject(FieldValidationService);
    protected commonService = inject(CommonService);
    protected messageService = inject(MessagesService);

    form: FormGroup;
    entity = signal<T | null>(null);
    isLoading: boolean = false;    

    /**
     * @param initialFormValues Objeto contendo os valores iniciais do formulário.
     */
    constructor(initialFormValues: { [key: string]: any } ) {
        this.entity.set(
            this.dialogConfig.data?.item as T 
            || this.activatedRoute.snapshot.data['entity'] as T
            || null
        );

        this.form = this.fb.group(initialFormValues);

        if (this.entity() !== null) {
            const formattedValues = this.formatValuesInObject(this.entity() as { [key: string]: any }, 'view');
            this.form.patchValue(formattedValues);
        }
    }

    /**
     * @summary Salva a entidade atual.
     * @description Se a entidade possui um ID, ela será atualizada. Caso contrário, uma nova entidade será criada.
     * Após o salvamento, o diálogo é fechado e a resposta da API é retornada. 
     * Em caso de erro, a propriedade isLoading é definida como false.
     */
    async salvar() {  
        this.isLoading = true;
        const formValue = this.formatValuesInObject(this.form.value, 'save');

        this.entity() !== null
            ? this.entity.update(e => ({ ...e, ...formValue } as T))
            : this.entity.set({ ...formValue } as T); 

        const id = (this.entity() as any).id;
        return this.getResponse(() => 
            id ? this.service.update(id, this.entity() as T) : this.service.create(this.entity() as T)
            ).then(response => {
                if (response.statusCode === StatusCode.OK) {
                    this.closeDialog(response);            
                } else {
                    this.messageService.showMessageFromReponse(response);
                }
                this.isLoading = false;
                return response; 
            }
        );         
    }

    async getResponse(response: () => Promise<ApiResponse>): Promise<ApiResponse> {
        try {
            const res = await response();
            return res;
        } catch (ex: any) {
            return ex.error;
        }
    }    

    /**
     * @summary Fecha o diálogo atual.
     * @param response Resposta da API a ser retornada ao fechar o diálogo.
     * @summary Fecha o diálogo atual.
     * @description Reseta o formulário, define isLoading como false e fecha o diálogo retornando a resposta da API, se fornecida.
     */
    closeDialog(response: ApiResponse | null = null) {
        this.form.reset();
        this.isLoading = false;
        this.dialogRef.close(response);
    }      

    /**
     * @summary Formata todas as datas de um objeto para o formato correto.
     * @description Ao carregar: converte strings ISO para Date objects (para o DatePicker).
     * Ao salvar: converte Date objects para strings yyyy-MM-dd (para o backend).
     * @param obj Objeto contendo os dados a serem formatados.
     * @param mode Modo de formatação ('view' ou 'save').
     * @returns Novo objeto com as datas formatadas.
     */
    private formatValuesInObject(obj: any, mode: EntityDetailMode): any {
            if (!obj || typeof obj !== 'object') return obj;

        const formattedFields: any = {};

        Object.keys(obj).forEach(key => {
            const value = obj[key];

            // Ignorar campos de sistema (readonly)
            const isSystemField = ['dataDeCriacao', 'dataDeAtualizacao'].includes(key);

            if (!isSystemField) {
                formattedFields[key] = mode === 'view'
                    ? (this.commonService.isDateString(value) ? new Date(value) : value)
                    : (this.commonService.isDateValue(value) ? this.commonService.formatDateToISO(value) : value);
            }            
        });

        return formattedFields;
    }       
}