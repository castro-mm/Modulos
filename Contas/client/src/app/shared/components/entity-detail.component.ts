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
    protected fb: FormBuilder = inject(FormBuilder); 
    protected service: EntityService<T> = inject(EntityService);
    protected activatedRoute = inject(ActivatedRoute);
    protected dialogRef: DynamicDialogRef = inject(DynamicDialogRef);
    protected dialogConfig: DynamicDialogConfig = inject(DynamicDialogConfig);

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
            this.form.patchValue(this.entity() as { [key: string]: any });
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

        if (this.entity() !== null) {
            this.entity.update(e => ({ ...e, ...this.form.value } as T));
        } else {
            this.entity.set({ ...this.form.value } as T); 
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
}