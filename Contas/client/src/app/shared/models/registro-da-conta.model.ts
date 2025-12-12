import { Entity } from "@/core/models/entity.model";
import { Credor } from "./credor.model";
import { Pagador } from "./pagador.model";
import { ArquivoDoRegistroDaConta } from "./arquivo-do-registro-da-conta.model";
import { StatusDaConta } from "../objects/enums";

export class RegistroDaConta extends Entity {
    mes: number = 0;
    ano: number = 0;
    credorId: number = 0;
    pagadorId: number = 0;
    codigoDeBarras: string = '';
    dataDeVencimento: Date = new Date();
    dataDePagamento?: Date;
    valor: number = 0;
    valorDosJuros?: number;
    valorDoDesconto?: number;
    valorTotal: number = 0;
    observacoes: string = '';
    status: StatusDaConta = StatusDaConta.Pendente;

    credor: Credor = null!;
    pagador: Pagador = null!;    
    diasParaVencer?: number;
    diasEmAtraso?: number;    
    periodo?: string;
    // Aqui vira a referencia dos arquivos anexados ao registro da conta (implementar apos validar o crud de registro da conta)
    arquivos?: ArquivoDoRegistroDaConta[] = [];
}