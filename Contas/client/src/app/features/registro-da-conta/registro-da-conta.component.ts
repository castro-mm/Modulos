import { Component, inject, OnInit, TemplateRef, viewChild } from '@angular/core';
import { RegistroDaContaDetailComponent } from './detail/registro-da-conta-detail.component';
import { BreadcrumbComponent } from "@/core/components/breadcrumb.component";
import { sharedConfig } from '@/shared/config/shared.config';
import { EntityService } from '@/core/services/entity.service';
import { EntityListComponent } from '@/core/components/entity-list.component';
import { RegistroDaContaService } from '@/shared/services/registro-da-conta.service';
import { RegistroDaConta } from '@/shared/models/registro-da-conta.model';
import { RegistroDaContaParams } from '@/shared/params/registro-da-conta.params';
import { CommonService } from '@/core/services/common.service';
import { SelectOption } from '@/core/types/select-option.type';
import { Validators } from '@angular/forms';
import { CredorService } from '@/shared/services/credor.service';
import { Credor } from '@/shared/models/credor.model';
import { StatusCode } from '@/core/objects/enums';
import { PagadorService } from '@/shared/services/pagador.service';
import { Pagador } from '@/shared/models/pagador.model';
import { MesPipe } from '@/shared/pipes/mes.pipe';
import { StatusDaContaPipe } from '@/shared/pipes/status-da-conta.pipe';
import { Tag } from 'primeng/tag';
import { Badge } from "primeng/badge";
import { Arquivo } from '@/shared/models/arquivo.model';
import { SafePipe } from '@/shared/pipes/safe.pipe';
import { RegistroDaContaArquivoComponent } from './registro-da-conta-arquivo/registro-da-conta-arquivo';
import { VisualizacaoDeArquivo } from '@/shared/types/visualizacao-de-arquivo.type';
import { TableListComponent } from '@/core/components/table-list.component';
import { TableColumn } from '@/core/types/table-column.type';
import { StatusDaConta } from '@/shared/objects/enums';
import { ArquivoService } from '@/shared/services/arquivo.service';

@Component({
    selector: 'app-registro-da-conta',
    imports: [...sharedConfig.imports, BreadcrumbComponent, TableListComponent, StatusDaContaPipe, Tag, Badge, SafePipe], 
    templateUrl: './registro-da-conta.component.html',
    providers: [{ provide: EntityService, useClass: RegistroDaContaService }]
})
export class RegistroDaContaComponent extends EntityListComponent<RegistroDaConta, RegistroDaContaParams, RegistroDaContaDetailComponent> implements OnInit {
    commonService = inject(CommonService);
    credorService = inject(CredorService);
    pagadorService = inject(PagadorService);
    arquivoService = inject(ArquivoService);
    
    statusTemplate = viewChild<TemplateRef<any>>('statusTemplate');
    arquivoTemplate = viewChild<TemplateRef<any>>('arquivoTemplate');

    mesOptions: SelectOption[] = [];
    anoOptions: SelectOption[] = [];
    credorOptions: SelectOption[] = [];
    pagadorOptions: SelectOption[] = [];
    statusDaContaOptions: SelectOption[] = [];
    
    exibirDialog: boolean = false;
    visualizacaoConfig: VisualizacaoDeArquivo | null = null; 
    
    cols: TableColumn[] = [
        { field: 'id', header: '#', width: '2rem', align: 'center', sortable: true },
        { field: 'periodo', header: 'Período', width: '5rem', align: 'left' },
        { field: 'credor.nomeFantasia', header: 'Credor', width: '10rem', sortable: true },
        { field: 'pagador.nome', header: 'Pagador', width: '10rem', sortable: true },
        { field: 'dataDeVencimento', header: 'Data de Vencimento', width: '8rem', type: 'date', format: 'dd/MM/yyyy', align: 'center', sortable: true },
        { field: 'valorTotal', header: 'Valor Total', width: '5rem', type: 'currency', format: 'BRL', align: 'right', sortable: true },
        { field: 'status', header: 'Status', width: '8rem', pipe: 'statusDaConta', align: 'center', template: 'statusTemplate', sortable: true },
        { field: 'arquivos', header: 'Arquivos', width: '8rem', align: 'center', template: 'arquivoTemplate', sortable: false },
        { field: 'dataDeAtualizacao', header: 'Data de Atualização', width: '8rem', type: 'date', format: 'dd/MM/yyyy HH:mm:ss', align: 'center', sortable: true },
    ];

    constructor() {
        super(
            { 
                mes: [new Date().getMonth(), [Validators.required]], 
                ano: [new Date().getFullYear(), [Validators.required]],
                credorId: [], 
                pagadorId: [],
                statusDaConta: [] 
            }, 
            RegistroDaContaDetailComponent, 
            '60%'
        );
    }

    get columnTemplates(): { [key: string]: TemplateRef<any> } {
        return {
            statusTemplate: this.statusTemplate()!,
            arquivoTemplate: this.arquivoTemplate()!,
        }
    }

    formatDate(dateString: string): string {
        return new Date(dateString).toLocaleDateString('pt-BR');
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
                this.credorOptions = items.map((x: Credor) => ({ value: x.id, label: x.nomeFantasia, icon: '' }));
            } else {
                this.messageService.showMessageFromReponse((response as any).error);
            }
        });
    }

    carregarListaDePagadores() {
        this.pagadorService.getAll().then(response => {
            if (response.statusCode === StatusCode.OK) {
                const items = response.data.items as Pagador[];
                this.pagadorOptions = items.map((x: Pagador) => ({ value: x.id, label: x.nome, icon: '' }));  
            } else {
                this.messageService.showMessageFromReponse((response as any).error);
            }
        });
    }

    getStatusIcon(status: number): string {
        switch (status) {
          case 0: return 'pi pi-clock';    // Pendente
          case 1: return 'pi pi-check';    // Pago
          case 2: return 'pi pi-times';    // Atrasado
          case 3: return 'pi pi-ban';      // Cancelado
          default: return 'pi pi-question'; // Ícone padrão (desconhecido)
        }
    }

    getStatusSeverity(status: number): string {
        switch (status) {
          case 0: return 'warn';      // Amarelo/laranja
          case 1: return 'success';      // Verde
          case 2: return 'danger';       // Vermelho
          case 3: return 'secondary';    // Cinza
          default: return 'secondary';    // Cor padrão (cinza)
        }
    }

    obterPrazoPorStatus(registroDaConta: RegistroDaConta, opt: number = 1): string {
        let dias = 0;
        switch (registroDaConta.status) {
            case StatusDaConta.Pendente: dias = registroDaConta.diasParaVencer ?? 0; break;
            case StatusDaConta.Vencida: dias = -(registroDaConta.diasEmAtraso ?? 0); break;            
        }

        return opt === 1         
            ? (dias === 0 ? 'Hoje' : (dias).toString())
            : (dias === 0 
                ? 'Vence hoje' 
                : ((dias > 0 
                    ? 'Vence em ' 
                    : ' Vencida há ') + Math.abs(dias) + ' dia(s).'));
    }    

    obterArquivo(registroDaConta: RegistroDaConta, modalidadeDoArquivo: number): Arquivo | null {
        var arquivoAnexado = registroDaConta.arquivos?.find(x => x.modalidadeDoArquivo === modalidadeDoArquivo)?.arquivo;
        return arquivoAnexado ?? null;
    }

    async visualizarArquivo(arquivo: Arquivo) {
        this.exibirDialog = true;
        this.visualizacaoConfig = {
            arquivoSelecionado: arquivo,
            urlArquivo: `data:${arquivo.tipo};base64,${arquivo.dados}`,
            isImagem: arquivo.tipo.startsWith('image/')
        };    
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

    async excluirArquivo(arquivo: Arquivo): Promise<void> {
        this.messageService.confirm({
            header: 'Confirmação',
            message: 'Tem certeza que deseja excluir este arquivo?',
            accept: async () => {
                const response = await this.arquivoService.delete(arquivo.id);
                if (response.statusCode === StatusCode.OK) {                    
                    this.messageService.showSuccess('Requisição bem sucedida.', 'Arquivo excluído com sucesso.');
                    this.listar();
                } else {
                    this.messageService.showMessageFromReponse(response);
                }            
            }
        });    
    }

    closeDialog(): void {
        this.visualizacaoConfig = null;
    }

    openArquivoDialog(id: number) {
        const onClose = this.modalService.openDialogPage(
            RegistroDaContaArquivoComponent as any, 
            'Upload de Arquivos',
            { 
                registroDaContaId: id,
            },
            '60%',
        );

        onClose.subscribe(
            async (response) => {
                if (response) {
                    this.messageService.showSuccess('Arquivo vinculado com sucesso.');
                }
                await this.listar();                    
            }
        );
    }
}
