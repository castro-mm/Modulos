import { Component, computed, effect, inject, output, signal } from '@angular/core';
import { Validators } from '@angular/forms';
import { sharedConfig } from '@/shared/config/shared.config';
import { EntityDetailComponent } from '@/core/components/entity-detail.component';
import { EntityService } from '@/core/services/entity.service';
import { RegistroDaConta } from '@/shared/models/registro-da-conta.model';
import { RegistroDaContaService } from '@/shared/services/registro-da-conta.service';
import { SelectOption } from '@/core/types/select-option.type';
import { CredorService } from '@/shared/services/credor.service';
import { PagadorService } from '@/shared/services/pagador.service';
import { StatusCode } from '@/core/objects/enums';
import { Credor } from '@/shared/models/credor.model';
import { Pagador } from '@/shared/models/pagador.model';
import { CodigoDeBarras } from '@/shared/types/codigo-de-barras.type';
import { CodigoDeBarrasService } from '@/shared/services/codigo-de-barras.service';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ApiResponse } from '@/core/types/api-response.type';
import { CredorDetailComponent } from '@/features/credor/detail/credor-detail.component';
import { PagadorDetailComponent } from '@/features/pagador/detail/pagador-detail.component';
import { MessagesService } from '@/core/services/messages.service';
import { ArquivoService } from '@/shared/services/arquivo.service';
import { RegistroDaContaArquivoComponent } from '../registro-da-conta-arquivo/registro-da-conta-arquivo';
import { FieldValidationMessageComponent } from '@/core/components/field-validation-message.component';

@Component({
    selector: 'app-registro-da-conta-detail.component',
    imports: [...sharedConfig.imports, FieldValidationMessageComponent, RegistroDaContaArquivoComponent],
    templateUrl: './registro-da-conta-detail.component.html',
    providers: [{ provide: EntityService, useClass: RegistroDaContaService }]
})
export class RegistroDaContaDetailComponent extends EntityDetailComponent<RegistroDaConta> {
    arquivoService = inject(ArquivoService);
    credorService = inject(CredorService);
    pagadorService = inject(PagadorService);
    codigoDeBarrasService = inject(CodigoDeBarrasService);

    mesOptions: SelectOption[] = [];
    anoOptions: SelectOption[] = [];
    credorOptions: SelectOption[] = [];
    pagadorOptions: SelectOption[] = [];

    valor = signal<number>(0);
    valorDosJuros = signal<number>(0);
    valorDoDesconto = signal<number>(0);
    valorTotal = computed(() => this.valor() + this.valorDosJuros() - this.valorDoDesconto());

    codigoDeBarras = signal<string>('');
    codigoDeBarrasInfo = signal<CodigoDeBarras | null>(null);

    modalidadeDoArquivoSelecionado: number = 0;    
    ref: DynamicDialogRef | null = null;

    fieldsLabels: {[key: string]: string} = {
        mes: 'Mês',
        ano: 'Ano',
        credorId: 'Credor',
        pagadorId: 'Pagador',
        descricao: 'Descrição', // Avaliar se este campo é mesmo necessário.
        codigoDeBarras: 'Código de Barras',
        valor: 'Valor',
        valorDosJuros: 'Valor dos Juros',
        valorDoDesconto: 'Valor do Desconto',
        valorTotal: 'Valor Total',
        dataDeVencimento: 'Data de Vencimento',
        dataDePagamento: 'Data de Pagamento',
        observacoes: 'Instruções/Observações'
    };

    constructor(private dialogService: DialogService) {
        super({
            mes: [new Date().getMonth(), [Validators.required]],
            ano: [new Date().getFullYear(), [Validators.required]],
            credorId: [, [Validators.required]],
            pagadorId: [, [Validators.required]],
            descricao: ['', [Validators.required, Validators.minLength(3)]],
            codigoDeBarras: ['', [Validators.required, Validators.minLength(10)]],
            valor: [, [Validators.required, Validators.min(0.01)]],
            valorDosJuros: [, [Validators.min(0)]],
            valorDoDesconto: [, [Validators.min(0)]],
            valorTotal: [, [Validators.required, Validators.min(0.01)]],
            dataDeVencimento: [, [Validators.required]],
            dataDePagamento: [],
            observacoes: ['']
        });

        if (this.entity()) {
            const entity = this.entity() as RegistroDaConta;
            this.valor.set(entity.valor);
            this.valorDosJuros.set(entity.valorDosJuros || 0);
            this.valorDoDesconto.set(entity.valorDoDesconto || 0);
            this.codigoDeBarras.set(entity.codigoDeBarras || '');
        }

        effect(() => {
            const codigo = this.codigoDeBarras();
            if (codigo && codigo.length >= 10) {
                this.analisarCodigoDeBarras(codigo);
            } else {
                this.codigoDeBarrasInfo.set(null);
            }
        });

        effect(() => {
            const valorTotal = this.valorTotal();
            this.form.get('valorTotal')?.setValue(valorTotal);
        });
    }

    ngOnInit(): void {
        this.mesOptions = this.commonService.getMonthsList();
        this.anoOptions = this.commonService.getYearsList(2025);

        this.carregarListaDeCredores();
        this.carregarListaDePagadores();
    }

    carregarListaDeCredores() {
        this.credorService.getAll().then(response => {
            if (response.statusCode === StatusCode.OK) {
                const items = response.result?.data.items as Credor[];
                this.credorOptions = items.map((x: Credor) => ({ value: x.id, label: x.nomeFantasia, icon: '' }));
            } else {
                this.messageService.showMessageFromReponse((response as any).error);
            }
        });
    }

    carregarListaDePagadores() {
        this.pagadorService.getAll().then(response => {
            if (response.statusCode === StatusCode.OK) {
                const items = response.result?.data.items as Pagador[];
                this.pagadorOptions = items.map((x: Pagador) => ({ value: x.id, label: x.nome, icon: '' }));
            } else {
                this.messageService.showMessageFromReponse((response as any).error);
            }
        });
    }

    // Métodos para atualizar os signals
    onValorChange(event: any): void {
        this.valor.set(event.value || 0);
    }

    onValorDosJurosChange(event: any): void {
        this.valorDosJuros.set(event.value || 0);
    }

    onValorDoDescontoChange(event: any): void {
        this.valorDoDesconto.set(event.value || 0);
    }

    zeraValores() {
        this.valor.set(0);
        this.valorDosJuros.set(0);
        this.valorDoDesconto.set(0);
    }

    async analisarCodigoDeBarras(codigo: string)  {
        const info = await this.codigoDeBarrasService.analisarCodigoBarras(codigo);
        this.codigoDeBarrasInfo.set(info);

        if (info.valido) {
            // Preenche valor automaticamente se disponível
            if (info.valor && info.valor > 0) {
                this.valor.set(info.valor);
                this.form.patchValue({ valor: info.valor }, { emitEvent: false });
            }

            // Preenche data de vencimento se disponível
            if (info.dataDeVencimento) {
                this.form.patchValue({ dataDeVencimento: info.dataDeVencimento }, { emitEvent: false });
            }
        }
    }

    formatarCodigoDeBarras() {
        const codigo = this.codigoDeBarras();
        const info = this.codigoDeBarrasInfo();

        if (codigo && info?.valido) {
            const formatado = this.codigoDeBarrasService.formatarCodigoBarras(
                codigo,
                info.tipo as 'bancario' | 'concessionaria'
            );

            // Atualiza o signal e o form
            this.codigoDeBarras.set(formatado);
            this.form.patchValue({ codigoDeBarras: formatado }, { emitEvent: false });
        }
    }

    /**
     * Atualiza o signal quando o campo de código de barras mudar
     */
    onCodigoDeBarrasChange(event: any): void {
        const valor = event.target?.value || '';
        this.codigoDeBarras.set(valor);

        // Também atualiza o form control para manter sincronizado
        this.form.patchValue({ codigoDeBarras: valor }, { emitEvent: false });
    }

    abrirModalDoCredor() {
        this.abrirModal(CredorDetailComponent, 'Credor - Novo Registro');

        this.ref?.onClose.subscribe((response: ApiResponse) => {
            if (response && response.statusCode === StatusCode.OK) {
                this.carregarListaDeCredores();
                this.messageService.showSuccess('Credor criado com sucesso.');
            }
        });
    }

    abrirModalDoPagador() {
        this.abrirModal(PagadorDetailComponent, 'Pagador - Novo Registro');  

        this.ref?.onClose.subscribe((response: ApiResponse) => {
            if (response && response.statusCode === StatusCode.OK) {
                this.carregarListaDePagadores();
                this.messageService.showSuccess('Pagador criado com sucesso.');
            }
        });
    }

    abrirModal(component: any, titulo: string = 'Novo Registro') {
        this.ref = this.dialogService.open(component, {
            header: titulo,
            width: '30%',
            closable: true,
            contentStyle: { overflow: 'auto' },
            baseZIndex: 10000
        });
    }

    onUploadResult(result: ApiResponse) {
        if (result && result.statusCode === StatusCode.OK) {
            this.messageService.showSuccess('Arquivo vinculado com sucesso.');
        } else {
            this.messageService.showMessageFromReponse(result, 'Erro ao vincular o arquivo.');
        }
    }
}