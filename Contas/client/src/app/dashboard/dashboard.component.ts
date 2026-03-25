import { QuantitativoDeContas, GastoMensalPorCredor, GastoPorSegmentoDoCredor } from '@/shared/models/dashboard.model';
import { DashboardService } from '@/shared/services/dashboard.service';
import { Component, inject, OnInit, signal, TemplateRef, viewChild } from '@angular/core';
import { CurrencyPipe, DecimalPipe } from '@angular/common';
import { RegistroDaContaService } from '@/shared/services/registro-da-conta.service';
import { RegistroDaConta } from '@/shared/models/registro-da-conta.model';
import { StatusCode } from '@/core/objects/enums';
import { TableColumn } from '@/core/types/table-column.type';
import { StatusDaConta } from '@/shared/objects/enums';
import { StatusDaContaPipe } from '@/shared/pipes/status-da-conta.pipe';
import { sharedConfig } from '@/shared/config/shared.config';
import { BreadcrumbComponent } from "@/core/components/breadcrumb.component";
import { ModalService } from '@/core/services/modal.service';
import { MessagesService } from '@/core/services/messages.service';
import { RegistroDaContaDetailComponent } from '@/features/registro-da-conta/detail/registro-da-conta-detail.component';
import { TableListComponent } from '@/core/components/table-list.component';
import { Tag } from 'primeng/tag';
import { Badge } from 'primeng/badge';
import { Arquivo } from '@/shared/models/arquivo.model';
import { ArquivoService } from '@/shared/services/arquivo.service';
import { VisualizacaoDeArquivo } from '@/shared/types/visualizacao-de-arquivo.type';
import { SafePipe } from '@/shared/pipes/safe.pipe';
import { RegistroDaContaArquivoComponent } from '@/features/registro-da-conta/registro-da-conta-arquivo/registro-da-conta-arquivo';
import { ChartModule } from 'primeng/chart';
import { SelectModule } from 'primeng/select';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-dashboard.component',
    imports: [...sharedConfig.imports, CurrencyPipe, DecimalPipe, BreadcrumbComponent, TableListComponent, StatusDaContaPipe, Tag, Badge, SafePipe, ChartModule, SelectModule, FormsModule],
    templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
    service = inject(DashboardService);
    registroDaContaService = inject(RegistroDaContaService);
    modalService = inject(ModalService);
    messageService = inject(MessagesService);
    arquivoService = inject(ArquivoService);

    quantitativoDeContas: QuantitativoDeContas | null = null;
    registros: RegistroDaConta[] = [];
    isLoading = signal<boolean>(false);

    statusTemplate = viewChild<TemplateRef<any>>('statusTemplate');
    arquivoTemplate = viewChild<TemplateRef<any>>('arquivoTemplate');

    exibirDialog: boolean = false;
    visualizacaoConfig: VisualizacaoDeArquivo | null = null;

    detailComponent = RegistroDaContaDetailComponent;
    modalSize = '60%';

    cols: TableColumn[] = [
        { field: 'id', header: '#', width: '2rem', align: 'center', sortable: true },
        { field: 'periodo', header: 'Período', width: '5rem', align: 'left' },
        { field: 'credor.nomeFantasia', header: 'Credor', width: '10rem', sortable: true },
        { field: 'pagador.nome', header: 'Pagador', width: '10rem', sortable: true },
        { field: 'dataDeVencimento', header: 'Data de Vencimento', width: '8rem', type: 'date', format: 'dd/MM/yyyy', align: 'center', sortable: true },
        { field: 'valorTotal', header: 'Valor Total', width: '5rem', type: 'currency', format: 'BRL', align: 'right', sortable: true },
        { field: 'status', header: 'Status', width: '8rem', pipe: 'statusDaConta', align: 'center', template: 'statusTemplate', sortable: true },
        { field: 'arquivos', header: 'Arquivos', width: '8rem', align: 'center', template: 'arquivoTemplate', sortable: false },
        { field: 'dataDeAtualizacao', header: 'Atualizado em', width: '8rem', type: 'date', format: 'dd/MM/yyyy HH:mm', align: 'center', sortable: true },
    ];

    filterFields = ['id', 'credor.nomeFantasia', 'pagador.nome'];

    // Gráfico de gastos mensais por credor
    gastoMensalChartData: any = null;
    gastoMensalChartOptions: any = null;
    anosDisponiveis: { label: string; value: number }[] = [];
    anoSelecionado: number = new Date().getFullYear();

    // Gráfico de gastos por segmento do credor (Doughnut)
    segmentoChartData: any = null;
    segmentoChartOptions: any = null;
    anosDisponiveisSegmento: { label: string; value: number }[] = [];
    anoSelecionadoSegmento: number = new Date().getFullYear();

    constructor() { }

    get columnTemplates(): { [key: string]: TemplateRef<any> } {
        return {
            statusTemplate: this.statusTemplate()!,
            arquivoTemplate: this.arquivoTemplate()!,
        };
    }

    ngOnInit(): void {
        this.configurarOpcoesDoGrafico();
        this.configurarOpcoesDoGraficoDoughnut();
        this.atualizarDashboard();

        setInterval(() => {
            this.atualizarDashboard();
        }, 10 * 100000) // 1 minuto
    }

    async atualizarDashboard() {
        this.getQuantitativoDeContas();
        this.getRegistrosDeContas();
        this.getGastoMensalPorCredor();
        this.getGastoPorSegmentoDoCredor();
    }

    async getQuantitativoDeContas() {
        const response = await this.service.getQuantitativoDeContas();
        if (response.result?.isSuccessful) {
            this.quantitativoDeContas = response.result.data as QuantitativoDeContas;
        } else {
            console.error('Erro ao obter quantitativo de contas:', response.result?.message ?? response.message);
        }
    }

    async getRegistrosDeContas() {
        this.isLoading.set(true);
        try {
            const response = await this.registroDaContaService.getAll();
            this.registros = response.result?.data.items
                .sort((a: RegistroDaConta, b: RegistroDaConta) => new Date(a.dataDeVencimento).getTime() - new Date(b.dataDeVencimento).getTime())
                .filter((x: RegistroDaConta) => x.status === StatusDaConta.Pendente || x.status === StatusDaConta.Vencida
                ) as RegistroDaConta[];
        } catch (ex) {
            console.error('Erro ao obter registros de contas:', ex);
        } finally {
            this.isLoading.set(false);
        }
    }

    async getGastoMensalPorCredor() {
        try {
            const response = await this.service.getGastoMensalPorCredor(this.anoSelecionado);
            if (response.result?.isSuccessful) {
                const dados = response.result.data as GastoMensalPorCredor;

                this.anosDisponiveis = dados.anosDisponiveis.map(ano => ({
                    label: ano.toString(),
                    value: ano
                }));

                this.montarDadosDoGrafico(dados);
            } else {
                console.error('Erro ao obter gastos mensais por credor:', response.result?.message ?? response.message);
            }
        } catch (ex) {
            console.error('Erro ao obter gastos mensais por credor:', ex);
        }
    }

    async getGastoPorSegmentoDoCredor() {
        try {
            const response = await this.service.getGastoPorSegmentoDoCredor(this.anoSelecionadoSegmento);
            if (response.result?.isSuccessful) {
                const dados = response.result.data as GastoPorSegmentoDoCredor;

                this.anosDisponiveisSegmento = dados.anosDisponiveis.map(ano => ({
                    label: ano.toString(),
                    value: ano
                }));

                this.montarDadosDoGraficoDoughnut(dados);
            } else {
                console.error('Erro ao obter gastos por segmento:', response.result?.message ?? response.message);
            }
        } catch (ex) {
            console.error('Erro ao obter gastos por segmento:', ex);
        }
    }

    async onAnoChange() {
        await this.getGastoMensalPorCredor();
    }

    async onAnoSegmentoChange() {
        await this.getGastoPorSegmentoDoCredor();
    }

    private readonly coresPaleta: string[] = [
        '#4285F4', '#EA4335', '#FBBC04', '#34A853', '#FF6D01',
        '#46BDC6', '#7B1FA2', '#C2185B', '#0097A7', '#689F38',
        '#F57F17', '#5C6BC0', '#00897B', '#D81B60', '#8E24AA',
    ];

    private montarDadosDoGrafico(dados: GastoMensalPorCredor) {
        const labels = ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'];

        const datasets = dados.credores.map((credor, index) => {
            const cor = this.coresPaleta[index % this.coresPaleta.length];
            return {
                label: credor.nomeFantasia,
                data: credor.valores,
                fill: false,
                borderColor: cor,
                backgroundColor: cor,
                tension: 0.3,
                pointRadius: 4,
                pointHoverRadius: 6,
            };
        });

        this.gastoMensalChartData = { labels, datasets };
    }

    private montarDadosDoGraficoDoughnut(dados: GastoPorSegmentoDoCredor) {
        const labels = dados.segmentos.map(s => s.nome);
        const valores = dados.segmentos.map(s => s.valorTotal);
        const cores = dados.segmentos.map((_, i) => this.coresPaleta[i % this.coresPaleta.length]);

        this.segmentoChartData = {
            labels,
            datasets: [{
                data: valores,
                backgroundColor: cores,
                hoverBackgroundColor: cores.map(c => c + 'CC'),
                borderWidth: 2,
                borderColor: '#ffffff',
            }]
        };
    }

    private configurarOpcoesDoGrafico() {
        this.gastoMensalChartOptions = {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'bottom',
                    labels: {
                        usePointStyle: true,
                        padding: 16,
                    }
                },
                tooltip: {
                    mode: 'index',
                    intersect: false,
                    callbacks: {
                        label: (context: any) => {
                            const valor = context.raw as number;
                            return `${context.dataset.label}: ${valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}`;
                        }
                    }
                }
            },
            scales: {
                x: {
                    grid: { display: false }
                },
                y: {
                    beginAtZero: true,
                    ticks: {
                        callback: (value: number) => value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL', minimumFractionDigits: 0 })
                    }
                }
            },
            interaction: {
                mode: 'nearest',
                axis: 'x',
                intersect: false,
            }
        };
    }

    private configurarOpcoesDoGraficoDoughnut() {
        this.segmentoChartOptions = {
            responsive: true,
            maintainAspectRatio: false,
            cutout: '55%',
            plugins: {
                legend: {
                    position: 'bottom',
                    labels: {
                        usePointStyle: true,
                        padding: 14,
                    }
                },
                tooltip: {
                    callbacks: {
                        label: (context: any) => {
                            const valor = context.raw as number;
                            const total = context.dataset.data.reduce((a: number, b: number) => a + b, 0);
                            const percentual = ((valor / total) * 100).toFixed(1);
                            return `${context.label}: ${valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })} (${percentual}%)`;
                        }
                    }
                }
            }
        };
    }

    getStatusIcon(status: number): string {
        switch (status) {
            case 0: return 'pi pi-clock';
            case 1: return 'pi pi-check';
            case 2: return 'pi pi-times';
            case 3: return 'pi pi-ban';
            default: return 'pi pi-question';
        }
    }

    getStatusSeverity(status: number): string {
        switch (status) {
            case 0: return 'warn';
            case 1: return 'success';
            case 2: return 'danger';
            case 3: return 'secondary';
            default: return 'secondary';
        }
    }

    obterPrazoPorStatus(registroDaConta: RegistroDaConta, opt: number = 1): string {
        let dias = 0;
        switch (registroDaConta.status) {
            case StatusDaConta.Pendente: dias = registroDaConta.diasParaVencer ?? 0; break;
            case StatusDaConta.Vencida: dias = -(registroDaConta.diasEmAtraso ?? 0); break;
        }

        return opt === 1
            ? (dias === 0 ? 'Hoje' : dias.toString())
            : (dias === 0
                ? 'Vence hoje'
                : ((dias > 0
                    ? 'Vence em '
                    : ' Vencida há ') + Math.abs(dias) + ' dia(s).'));
    }

    formatDate(dateString: string): string {
        return new Date(dateString).toLocaleDateString('pt-BR');
    }

    obterArquivo(registroDaConta: RegistroDaConta, modalidadeDoArquivo: number): Arquivo | null {
        const arquivoAnexado = registroDaConta.arquivos?.find(x => x.modalidadeDoArquivo === modalidadeDoArquivo)?.arquivo;
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
            setTimeout(() => { window.URL.revokeObjectURL(url); }, 5000);
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
                    this.getRegistrosDeContas();
                } else {
                    this.messageService.showMessageFromReponse(response);
                }
            }
        });
    }

    openArquivoDialog(id: number) {
        const onClose = this.modalService.openDialogPage(
            RegistroDaContaArquivoComponent as any,
            'Upload de Arquivos',
            { registroDaContaId: id },
            '60%'
        );

        onClose.subscribe(async (response) => {
            if (response) {
                this.messageService.showSuccess('Arquivo vinculado com sucesso.');
            }
            await this.atualizarDashboard();
        });
    }

    async excluir(item: RegistroDaConta) {
        this.messageService.confirm({
            header: 'Confirmação',
            message: 'Tem certeza que deseja excluir este registro?',
            accept: async () => {
                const response = await this.registroDaContaService.delete(item.id);
                if (response.statusCode === StatusCode.OK) {
                    this.messageService.showSuccess('Registro excluído com sucesso.');
                    this.getRegistrosDeContas();
                } else {
                    this.messageService.showMessageFromReponse(response);
                }
            }
        });
    }

    async excluirVarios(ids: number[]) {
        this.messageService.confirm({
            header: 'Confirmação',
            message: `Tem certeza que deseja excluir ${ids.length} registro(s)?`,
            accept: async () => {
                const response = await this.registroDaContaService.deleteRange(ids);
                if (response.statusCode === StatusCode.OK) {
                    this.messageService.showSuccess('Registros excluídos com sucesso.');
                    this.atualizarDashboard();
                } else {
                    this.messageService.showMessageFromReponse(response);
                }
            }
        });
    }
}
