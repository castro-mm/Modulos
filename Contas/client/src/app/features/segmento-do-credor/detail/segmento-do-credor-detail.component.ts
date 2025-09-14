import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { SegmentoDoCredor } from '../../../shared/models/segmento-do-credor.model';
import { ApiResponse } from '@/core/models/api-response.model';
import { MessagesService } from '@/core/services/messages.service';
import { sharedConfig } from '@/shared/config/shared.config';
import { SegmentoDoCredorService } from '@/shared/services/segmento-do-credor.service';

@Component({
    imports: [...sharedConfig.imports],
    templateUrl: './segmento-do-credor-detail.component.html',
    providers: [SegmentoDoCredorService]
})
export class SegmentoDoCredorDetailComponent {
    form: FormGroup;
    item: SegmentoDoCredor | null = null;
    isLoading: boolean = false;

    constructor(
        private fb: FormBuilder, 
        private dialogRef: DynamicDialogRef, 
        private dialogConfig: DynamicDialogConfig, 
        private segmentoDoCredorService: SegmentoDoCredorService, 
        private messageService: MessagesService
    ) {          
        this.item = this.dialogConfig.data?.item as SegmentoDoCredor || null;
        this.form = this.fb.group({
            nome: [this.item?.nome || '', [Validators.required, Validators.minLength(3)]]
        });
    }

    async salvar() {        
        this.isLoading = true;

        this.item = this.form.value as SegmentoDoCredor;
        if (this.dialogConfig.data?.item) this.item.id = this.dialogConfig.data?.item.id;

        const response = !this.item.id 
            ? await this.segmentoDoCredorService.create(this.item) 
            : await this.segmentoDoCredorService.update(this.item.id, this.item);

        this.closeDialog(response);
    }

    closeDialog(response: ApiResponse | null = null) {
        this.form.reset();
        this.isLoading = false;
        this.dialogRef.close(response);
    }
}
