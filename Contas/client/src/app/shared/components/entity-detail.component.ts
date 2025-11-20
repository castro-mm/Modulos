import { ApiResponse } from "@/core/models/api-response.model";
import { Entity } from "@/core/models/entity.model";
import { EntityService } from "@/core/services/entity.service";
import { inject, signal } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { DynamicDialogConfig, DynamicDialogRef } from "primeng/dynamicdialog";

/**
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
    form: FormGroup;
    entity = signal<T | null>(null);
    isLoading: boolean = false;

    /**
     * @summary Injeção de dependências.
     * @description Utiliza o sistema de injeção de dependências do Angular para obter instâncias necessárias.
     */
    protected fb = inject(FormBuilder); 
    protected service = inject(EntityService);
    protected activatedRoute = inject(ActivatedRoute);
    protected dialogRef = inject(DynamicDialogRef);
    protected dialogConfig = inject(DynamicDialogConfig);

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
            const formattedEntity = this.formatDatesInObject(this.entity() as { [key: string]: any }, true);
            this.form.patchValue(formattedEntity);
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

        const formValue = this.formatDatesInObject(this.form.value);

        if (this.entity() !== null) {
            this.entity.update(e => ({ ...e, ...formValue } as T));
        } else {
            this.entity.set({ ...formValue } as T); 
        }

        const id = (this.entity() as any).id;
        const response = !id
            ? await this.service.create(this.entity()!)
            : await this.service.update(id, this.entity()!);

        this.closeDialog(response);
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
     * @param toDateObject Se true, converte strings para Date. Se false, converte Date para string.
     * @returns Novo objeto com as datas formatadas.
     */
    private formatDatesInObject(obj: any, toDateObject: boolean = false): any {
        if (!obj || typeof obj !== 'object') return obj;

        const formatted: any = {};

        Object.keys(obj).forEach(key => {
            const value = obj[key];

            // Ignorar campos de sistema (readonly)
            const isSystemField = ['dataDeCriacao', 'dataDeAtualizacao'].includes(key);

            if (toDateObject && !isSystemField) {
                // Ao carregar: converter strings ISO para Date objects
                if (this.isDateString(value)) {
                    formatted[key] = new Date(value);
                } else {
                    formatted[key] = value;
                }
            } else if (!toDateObject && !isSystemField) {
                // Ao salvar: converter Date objects para strings yyyy-MM-dd
                if (this.isDateValue(value)) {
                    formatted[key] = this.formatDateToISO(value);
                } else {
                    formatted[key] = value;
                }
            }
        });

        return formatted;
    }

    /**
     * @summary Verifica se um valor é uma string de data ISO.
     * @description Identifica strings no formato ISO (yyyy-MM-dd ou yyyy-MM-ddTHH:mm:ss).
     * @param value Valor a ser verificado.
     * @returns true se for uma string de data ISO, false caso contrário.
     */
    private isDateString(value: any): boolean {
        if (!value || typeof value !== 'string') return false;

        // Verificar se é uma string ISO com ou sem hora
        return value.match(/^\d{4}-\d{2}-\d{2}(T|$)/) !== null;
    }

    /**
     * @summary Verifica se um valor é uma data válida (Date object).
     * @description Identifica Date objects que precisam ser convertidos para string.
     * @param value Valor a ser verificado.
     * @returns true se for um Date object válido, false caso contrário.
     */
    private isDateValue(value: any): boolean {
        if (!value) return false;

        // Verificar se é um Date object válido
        return value instanceof Date && !isNaN(value.getTime());
    }

    /**
     * @summary Converte uma data para o formato yyyy-MM-dd.
     * @description Extrai apenas a parte da data (sem hora) de um Date object.
     * @param date Data a ser formatada (Date object).
     * @returns String no formato yyyy-MM-dd.
     */
    private formatDateToISO(date: Date): string {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        
        return `${year}-${month}-${day}`;
    }
}