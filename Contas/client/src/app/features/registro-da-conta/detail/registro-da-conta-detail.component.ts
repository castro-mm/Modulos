import { Component, computed, effect, inject, signal, Type } from '@angular/core';
import { Validators } from '@angular/forms';
import { sharedConfig } from '@/shared/config/shared.config';
import { EntityDetailComponent } from '@/shared/components/entity-detail.component';
import { EntityService } from '@/core/services/entity.service';
import { RegistroDaConta } from '@/shared/models/registro-da-conta.model';
import { RegistroDaContaService } from '@/shared/services/registro-da-conta.service';
import { KeyValuePair } from '@/core/models/key-value-pair.model';
import { CommonService } from '@/core/services/common.service';
import { CredorService } from '@/shared/services/credor.service';
import { PagadorService } from '@/shared/services/pagador.service';
import { StatusCode } from '@/core/objects/enums';
import { Credor } from '@/shared/models/credor.model';
import { Pagador } from '@/shared/models/pagador.model';
import { CodigoDeBarras } from '@/shared/types/codigo-de-barras.type';
import { CodigoDeBarrasService } from '@/shared/services/codigo-de-barras.service';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ModalService } from '@/core/services/modal.service';
import { ApiResponse } from '@/core/models/api-response.model';
import { CredorDetailComponent } from '@/features/credor/detail/credor-detail.component';
import { PagadorDetailComponent } from '@/features/pagador/detail/pagador-detail.component';

@Component({
    selector: 'app-registro-da-conta-detail.component',
    imports: [...sharedConfig.imports],
    templateUrl: './registro-da-conta-detail.component.html',
    providers: [{ provide: EntityService, useClass: RegistroDaContaService }]
})
export class RegistroDaContaDetailComponent extends EntityDetailComponent<RegistroDaConta> {
    commonService = inject(CommonService);
    credorService = inject(CredorService);
    pagadorService = inject(PagadorService);
    codigoDeBarrasService = inject(CodigoDeBarrasService);

    mesOptions: KeyValuePair[] = [];
    anoOptions: KeyValuePair[] = [];
    credorOptions: KeyValuePair[] = [];
    pagadorOptions: KeyValuePair[] = [];
    messageService: any;

    valor = signal<number>(0);
    valorDosJuros = signal<number>(0);
    valorDoDesconto = signal<number>(0);
    valorTotal = computed(() => this.valor() + this.valorDosJuros() - this.valorDoDesconto());

    codigoDeBarras = signal<string>('');
    codigoDeBarrasInfo = signal<CodigoDeBarras | null>(null);

    ref: DynamicDialogRef | null = null;

    constructor(private dialogService: DialogService) {
        super({
            mes: [new Date().getMonth(), [Validators.required]],
            ano: [new Date().getFullYear(), [Validators.required]],
            credorId: [null, [Validators.required]],
            pagadorId: [null, [Validators.required]],
            descricao: ['', [Validators.required, Validators.minLength(3)]],
            codigoDeBarras: ['', [Validators.required, Validators.minLength(10)]],
            valor: [, [Validators.required, Validators.min(0.01)]],
            valorDosJuros: [0],
            valorDoDesconto: [0],
            valorTotal: [{ value: 0, disabled: true }, [Validators.required, Validators.min(0.01)]],
            dataDeVencimento: [null, [Validators.required]],
            dataDePagamento: [null],
            observacoes: ['']
        });

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
                const items = response.data.items as Credor[];
                this.credorOptions = items.map((x: Credor) => ({ key: x.id, value: x.nomeFantasia }));
            } else {
                this.messageService.showMessageFromReponse((response as any).error);
            }
        });
    }

    carregarListaDePagadores() {
        this.pagadorService.getAll().then(response => {
            if (response.statusCode === StatusCode.OK) {
                const items = response.data.items as Pagador[];
                this.pagadorOptions = items.map((x: Pagador) => ({ key: x.id, value: x.nome }));
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

            // Adiciona informações na descrição se estiver vazia
            if (!this.form.get('descricao')?.value) {
                const descricaoAuto = info.tipo === 'bancario'
                    ? `Boleto ${info.banco}`
                    : `Conta de ${info.detalhes?.produto}`;

                this.form.patchValue({ descricao: descricaoAuto }, { emitEvent: false });
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
        this.abrirModal(CredorDetailComponent);

        this.ref?.onClose.subscribe((response: ApiResponse) => {
            if (response && response.statusCode === StatusCode.OK) {
                this.carregarListaDeCredores();
                this.messageService.showSuccess('Credor criado com sucesso.');
            }
        });
    }

    abrirModalDoPagador() {
        this.abrirModal(PagadorDetailComponent);  

        this.ref?.onClose.subscribe((response: ApiResponse) => {
            if (response && response.statusCode === StatusCode.OK) {
                this.carregarListaDePagadores();
                this.messageService.showSuccess('Pagador criado com sucesso.');
            }
        });
    }

    abrirModal(component: any) {
        this.ref = this.dialogService.open(component, {
            header: 'Novo Registro',
            width: '30%',
            contentStyle: { overflow: 'auto' },
            baseZIndex: 10000
        });
    }
}