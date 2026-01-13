import { sharedConfig } from '@/shared/config/shared.config';
import { Component, inject, input, output, signal } from '@angular/core';
import { FileUploadHandlerEvent } from 'primeng/fileupload';
import { ArquivoService } from '../../shared/services/arquivo.service';
import { ApiResponse } from '@/core/types/api-response.type';
import { environment } from 'src/environments/environment.development';

@Component({
    selector: 'app-upload',
    imports: [...sharedConfig.imports],
    template: `
        <div>
            <p-fileUpload 
                #fu
                name="file"
                mode="basic"
                chooseLabel="Selecionar Arquivo"
                uploadLabel="Enviar"
                cancelLabel="Cancelar"
                [auto]="true"
                [maxFileSize]="maxFileSize"
                [accept]="acceptedFileTypes"
                [customUpload]="true" 
                (uploadHandler)="onUpload($event)"
                [disabled]="disabled()"
            />
            @if (uploading()) {
                <p-progressbar class="mt-2" mode="indeterminate" [style]="{'height': '5px'}" />
            }
        </div>
    `
})
/**
 * @author Marcelo M. de Castro
 * @summary Componente para upload de arquivos com suporte a tipos e tamanhos configuráveis, exibindo barra de progresso durante o envio.
 * @version 1.0.0
 */
export class UploadComponent {
    protected service = inject(ArquivoService);

    disabled = input<boolean>(false); 
    uploadResult = output<ApiResponse>();
    uploading = signal<boolean>(false);

    maxFileSize: number = environment.file.maxFileSizeInBytes;
    acceptedFileTypes: string = environment.file.allowedFileTypes;

    constructor() { }
    
    /**
     * @summary Manipula o evento de upload de arquivo, enviando o arquivo selecionado para o serviço de upload.
     * @param event Evento contendo os arquivos selecionados para upload.
     */
    onUpload(event: FileUploadHandlerEvent) {        
        this.uploading.set(true);
        this.service.upload(event.files[0])
            .then((response: ApiResponse) => {
                return this.uploadResult.emit(response)
            })
            .catch(error => this.onError(error))
            .finally(() => this.uploading.set(false));
    }

    /** 
     * @summary Manipula erros durante o upload de arquivos.
     * @param error Erro ocorrido durante o upload.
     */
    onError(error: any) {
        this.uploadResult.emit(error);
    }
}
