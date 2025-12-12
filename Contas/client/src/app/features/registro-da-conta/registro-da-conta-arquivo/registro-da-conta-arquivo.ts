import { ApiResponse } from '@/core/types/api-response.type';
import { StatusCode } from '@/core/objects/enums';
import { MessagesService } from '@/core/services/messages.service';
import { FieldValidationMessageComponent } from '@/core/components/field-validation-message.component';
import { UploadComponent } from '@/core/components/upload.component';
import { sharedConfig } from '@/shared/config/shared.config';
import { ArquivoDoRegistroDaConta } from '@/shared/models/arquivo-do-registro-da-conta.model';
import { Arquivo } from '@/shared/models/arquivo.model';
import { FileSizePipe } from '@/shared/pipes/file-size.pipe';
import { SafePipe } from '@/shared/pipes/safe.pipe';
import { ArquivoDoRegistroDaContaService } from '@/shared/services/arquivo-do-registro-da-conta.service';
import { ArquivoService } from '@/shared/services/arquivo.service';
import { VisualizacaoDeArquivo } from '@/shared/types/visualizacao-de-arquivo.type';
import { Component, computed, inject, model, OnInit, output, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { SelectOption } from '@/core/types/select-option.type';

@Component({
    selector: 'app-registro-da-conta-arquivo',
    imports: [...sharedConfig.imports, UploadComponent, SafePipe, FileSizePipe],
    templateUrl: './registro-da-conta-arquivo.html',
    styleUrls: ['./registro-da-conta-arquivo.scss']
})
export class RegistroDaContaArquivoComponent implements OnInit {
    service = inject(ArquivoDoRegistroDaContaService);
    arquivoService = inject(ArquivoService);
    dialogConfig = inject(DynamicDialogConfig);
    dialogRef = inject(DynamicDialogRef);
    activatedRoute = inject(ActivatedRoute);
    messageService = inject(MessagesService);

    registroDaContaId = model<number>(0);
    uploadResult = output<ApiResponse>();

    modalidadeDoArquivo = signal<number>(0);
    listaDeArquivosDoRegistroDaConta = signal<ArquivoDoRegistroDaConta[]>([]);

    modalidadeDoArquivoOptions: SelectOption[] = [];
    modalidadeDoArquivoSelected = computed(() => this.modalidadeDoArquivo() > 0);

    isDialog = computed(() => !!this.dialogConfig.data.registroDaContaId);

    exibirDialog: boolean = false;
    visualizacaoConfig: VisualizacaoDeArquivo | null = null; 

    constructor() {        
        this.registroDaContaId.set(this.dialogConfig.data?.registroDaContaId ?? 0);
    }

    ngOnInit(): void { 
        if (this.registroDaContaId() === 0) {
            this.messageService.showError('ID do Registro da Conta não fornecido.');
            return;
        }
        this.inicializaEstadoDoComponente();
    }

    inicializaEstadoDoComponente() {
        this.exibirDialog = false;
        this.visualizacaoConfig = {arquivoSelecionado: null, urlArquivo: '', isImagem: false};
        this.carregarListaDeArquivos();
        this.modalidadeDoArquivoOptions = [];
        this.obterModalidadeDoArquivoOptions();
        this.modalidadeDoArquivo.set(0);
    }    

    async obterModalidadeDoArquivoOptions(): Promise<void> {
        const response = await this.service.getArquivosByRegistroDaContaId(this.registroDaContaId());
        if (response.statusCode === StatusCode.OK) {
            const arquivosExistentes = response.data as ArquivoDoRegistroDaConta[];                    
            const modalidadesExistentes = arquivosExistentes.map(a => a.modalidadeDoArquivo);
            this.modalidadeDoArquivoOptions = this.service.getModalidadesDeArquivo().filter(m => !modalidadesExistentes.includes(m.value));
            
        } else {
            this.messageService.showError('Erro ao carregar modalidades de arquivo.');
        }
    }

    // Nova proposta de solução para 
    async onUploadResult(response: ApiResponse): Promise<void> {
        const arquivo = response.data as Arquivo;
        if (!arquivo) {
            return;
        }
        let registroDaContaArquivo = new ArquivoDoRegistroDaConta(
            this.registroDaContaId(),
            arquivo.id,
            this.modalidadeDoArquivo()
        );

        const result = await this.service.create(registroDaContaArquivo);
        this.uploadResult.emit(result);
        this.inicializaEstadoDoComponente();

        if (this.isDialog()) {
            this.dialogRef.close(result);
        }
    }
    
    async baixarArquivo(arquivo: Arquivo): Promise<void> {
        const response = await this.arquivoService.download(arquivo.id);
        if (response) {
            const url = window.URL.createObjectURL(response);
            const link = document.createElement('a');

            link.href = url;
            link.download = `${arquivo.nome}`;
            link.click();
            
            // Limpa a URL criada após um curto período para garantir que o arquivo seja carregado
            setTimeout(() => { window.URL.revokeObjectURL(url); }, 5000); // 5 segundos
        }
    }    

    visualizarArquivo(arquivo: Arquivo): void {
        this.exibirDialog = true;
        this.visualizacaoConfig = {
            arquivoSelecionado: arquivo,
            urlArquivo: `data:${arquivo.tipo};base64,${arquivo.dados}`,
            isImagem: arquivo.tipo.startsWith('image/')
        };
    }

    async excluirArquivo(arquivo: Arquivo): Promise<void> {
        this.messageService.confirm({
            header: 'Confirmação',
            message: 'Tem certeza que deseja excluir este arquivo?',
            accept: async () => {
                const response = await this.arquivoService.delete(arquivo.id);
                if (response.statusCode === StatusCode.OK) {                    
                    this.messageService.showSuccess('Requisição bem sucedida.', 'Arquivo excluído com sucesso.');
                    this.inicializaEstadoDoComponente();
                } else {
                    this.uploadResult.emit(response);
                }            
            }
        });    
    }

    async carregarListaDeArquivos(): Promise<void> {
        const response = await this.service.getArquivosByRegistroDaContaId(this.registroDaContaId());
        if (response.statusCode === StatusCode.OK) {            
            this.listaDeArquivosDoRegistroDaConta.set(
                (response.data).map(
                    (arc: ArquivoDoRegistroDaConta) => {
                        return {
                            ...arc,
                            modalidadeDoArquivo: this.service.getModalidadesDeArquivo().find(t => t.value === arc.modalidadeDoArquivo)?.label || 'Desconhecido',
                            tamanho: (arc.arquivo.tamanho / 1024).toFixed(2)
                        }
                    }
                )
            );            
        }
    }
}
