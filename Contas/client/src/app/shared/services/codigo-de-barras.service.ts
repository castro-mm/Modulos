import { inject, Injectable } from '@angular/core';
import { CodigoDeBarras } from '../types/codigo-de-barras.type';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { BancoService } from './banco.service';

@Injectable({
    providedIn: 'root'
})
export class CodigoDeBarrasService {
    bancoService = inject(BancoService);

    constructor() { }

    public async analisarCodigoBarras(codigo: string): Promise<CodigoDeBarras> {
        // Remove espaços, pontos, hífens
        const codigoLimpo = codigo.replace(/[\s.\-]/g, '');

        // Valida se contém apenas números
        if (!/^\d+$/.test(codigoLimpo)) {
            return {
                tipo: 'invalido',
                valido: false,
                mensagem: 'Código de barras deve conter apenas números'
            };
        }

        // Identifica o tipo pelo tamanho
        if (codigoLimpo.length === 47) {
            return await this.analisarBoletoBancario(codigoLimpo);
        } else if (codigoLimpo.length === 48) {
            return this.analisarConcessionaria(codigoLimpo);
        } else if (codigoLimpo.length === 44) {
            // Código de barras direto do boleto bancário
            return await this.analisarCodigoBarrasBancario(codigoLimpo);
        } else {
            return {
                tipo: 'invalido',
                valido: false,
                mensagem: `Tamanho inválido: ${codigoLimpo.length} dígitos (esperado 44, 47 ou 48)`
            };
        }
    }

    /**
     * Analisa linha digitável de boleto bancário (47 dígitos)
     */
    private async analisarBoletoBancario(linha: string): Promise<CodigoDeBarras> {
        try {
            // Extrai os campos da linha digitável
            const campo1 = linha.substring(0, 10);  // 10 dígitos (9 + 1 DV)
            const campo2 = linha.substring(10, 21); // 11 dígitos (10 + 1 DV)
            const campo3 = linha.substring(21, 32); // 11 dígitos (10 + 1 DV)
            const campo4 = linha.substring(32, 33); // 1 dígito (DV geral)
            const campo5 = linha.substring(33, 47); // 14 dígitos (fator + valor)

            // Reconstrói o código de barras
            const codigoBanco = campo1.substring(0, 3);
            const moeda = campo1.substring(3, 4);
            const dvGeral = campo4;
            const fatorVencimento = campo5.substring(0, 4);
            const valorStr = campo5.substring(4, 14);

            // Valida DV dos campos
            if (!this.validarDVCampo(campo1.substring(0, 9), campo1.substring(9, 10))) {
                return {
                    tipo: 'bancario',
                    valido: false,
                    mensagem: 'Dígito verificador do campo 1 inválido'
                };
            }

            if (!this.validarDVCampo(campo2.substring(0, 10), campo2.substring(10, 11))) {
                return {
                    tipo: 'bancario',
                    valido: false,
                    mensagem: 'Dígito verificador do campo 2 inválido'
                };
            }

            if (!this.validarDVCampo(campo3.substring(0, 10), campo3.substring(10, 11))) {
                return {
                    tipo: 'bancario',
                    valido: false,
                    mensagem: 'Dígito verificador do campo 3 inválido'
                };
            }

            // Calcula valor
            const valor = parseInt(valorStr) / 100;

            // Calcula vencimento
            const vencimento = this.calcularVencimento(parseInt(fatorVencimento));

            // Nome do banco
            const banco = await this.bancoService.obterBancoPorCodigo(codigoBanco);
            if (!banco) {
                return {
                    tipo: 'bancario',
                    valido: false,
                    mensagem: `Banco com código ${codigoBanco} não encontrado`
                };
            }
            
            return {
                tipo: 'bancario',
                valido: true,
                valor: valor,
                dataDeVencimento: vencimento,
                banco: banco.name,
                codigoDoBanco: codigoBanco,
                mensagem: `Boleto ${banco.name} - Data de Vencimento: ${vencimento.toLocaleDateString('pt-BR')} - Valor: R$ ${valor.toFixed(2)}`,
                detalhes: {
                    moeda: moeda === '9' ? 'Real' : 'Desconhecida',
                    fatorDeVencimento: parseInt(fatorVencimento)
                }
            };

        } catch (error) {
            return {
                tipo: 'bancario',
                valido: false,
                mensagem: 'Erro ao analisar boleto bancário'
            };
        }
    }

    /**
     * Analisa código de barras direto do boleto (44 dígitos)
     */
    private async analisarCodigoBarrasBancario(codigo: string): Promise<CodigoDeBarras> {
        try {
            const codigoBanco = codigo.substring(0, 3);
            const moeda = codigo.substring(3, 4);
            const dvGeral = codigo.substring(4, 5);
            const fatorVencimento = codigo.substring(5, 9);
            const valorStr = codigo.substring(9, 19);

            const valor = parseInt(valorStr) / 100;
            const vencimento = this.calcularVencimento(parseInt(fatorVencimento));

            // Nome do banco
            const banco = await this.bancoService.obterBancoPorCodigo(codigoBanco);
            if (!banco) {
                return {
                    tipo: 'bancario',
                    valido: false,
                    mensagem: `Banco com código ${codigoBanco} não encontrado`
                };
            }

            return {
                tipo: 'bancario',
                valido: true,
                valor: valor,
                dataDeVencimento: vencimento,
                banco: banco.name,
                codigoDoBanco: codigoBanco,
                mensagem: `Boleto ${banco.name} - Data de Vencimento: ${vencimento.toLocaleDateString('pt-BR')} - Valor: R$ ${valor.toFixed(2)}`,
                detalhes: {
                    moeda: moeda === '9' ? 'Real' : 'Desconhecida',
                    fatorDeVencimento: parseInt(fatorVencimento)
                }
            };

        } catch (error) {
            return {
                tipo: 'bancario',
                valido: false,
                mensagem: 'Erro ao analisar código de barras bancário'
            };
        }
    }

    /**
     * Analisa código de barras de concessionária (48 dígitos)
     */
    private analisarConcessionaria(codigo: string): CodigoDeBarras {
        try {
            // Divide em 4 blocos de 12 dígitos
            const bloco1 = codigo.substring(0, 12);
            const bloco2 = codigo.substring(12, 24);
            const bloco3 = codigo.substring(24, 36);
            const bloco4 = codigo.substring(36, 48);

            // Analisa primeiro bloco
            const produto = bloco1.substring(0, 1);
            const segmento = bloco1.substring(1, 2);
            const empresa = bloco1.substring(2, 4);
            const identificadorDoValor = bloco1.substring(2, 3);

            let valor: number | undefined = undefined;

            if (['6', '7', '8', '9'].indexOf(identificadorDoValor)) {
                const camposDeIdentificacao = bloco1.substring(0, 4);

                const campoLivre = 
                    bloco1.substring(4,11) + 
                    bloco2.substring(0,11) + 
                    bloco3.substring(0,11) + 
                    bloco4.substring(0,11);

                const valorStr = campoLivre.substring(0, 11);
                const valorNumerico = parseInt(valorStr) / 100;

                if (!isNaN(valorNumerico) && valorNumerico > 0) {
                    valor = valorNumerico;
                }

                console.log(
                    `Campo de Identificação: ${camposDeIdentificacao}\n` +
                    `Campo Livre: ${campoLivre}\n` +
                    `Valor Str: ${valorStr}\n` +
                    `Valor: ${valorNumerico}\n` +
                    `Identificador do Valor: ${identificadorDoValor}`
                );
            }

            // Valida DVs (módulo 10 ou 11)
            const dvBloco1 = bloco1.substring(11, 12);
            const dvBloco2 = bloco2.substring(11, 12);
            const dvBloco3 = bloco3.substring(11, 12);
            const dvBloco4 = bloco4.substring(11, 12);

            // Mapa de produtos
            const produtos: { [key: string]: string } = {
                '1': 'Prefeituras',
                '2': 'Saneamento',
                '3': 'Energia Elétrica e Gás',
                '4': 'Telecomunicações',
                '5': 'Órgãos Governamentais',
                '6': 'Carnes e Assemelhados',
                '7': 'Multas de Trânsito',
                '8': 'Uso exclusivo do banco',
                '9': 'Uso exclusivo do banco'
            };

            const segmentos: { [key: string]: string } = {
                '1': 'Prefeitura',
                '2': 'Saneamento',
                '3': 'Energia Elétrica',
                '4': 'Telecomunicações',
                '5': 'Órgãos Governamentais',
                '6': 'Multas de Trânsito',
                '7': 'Outros',
                '8': 'Exclusivo banco',
                '9': 'Exclusivo banco'
            };

            // Temporario (retirar depois)
            console.log(
                `Bloco 1: ${bloco1}\n` +
                `Bloco 2: ${bloco2}\n` +
                `Bloco 3: ${bloco3}\n` +
                `Bloco 4: ${bloco4}\n` +
                `Produto: ${produto} - ${produtos[produto]}\n` +
                `Segmento: ${segmento} - ${segmentos[segmento]}\n` +
                `Empresa/Convenio: ${empresa}\n` +
                `Identificador de Valor: ${identificadorDoValor}\n` +
                `Campo Livre: ${bloco1.substring(3, 11)}\n` +
                `DV: ${bloco1.substring(11, 12)}\n` 
            );

            const tipoProduto = produtos[produto] || 'Desconhecido';
            const tipoSegmento = segmentos[segmento] || 'Desconhecido';

            return {
                tipo: 'concessionaria',
                valido: true,     
                valor: valor,                    
                mensagem: `Conta de ${tipoProduto} - Segmento: ${tipoSegmento}`,
                detalhes: {
                    produto: tipoProduto,
                    segmento: tipoSegmento
                }
            };
        } catch (error) {
            return {
                tipo: 'concessionaria',
                valido: false,
                mensagem: 'Erro ao analisar código de concessionária'
            };
        }
    }

    /**
     * Valida dígito verificador de um campo (módulo 10)
     */
    private validarDVCampo(campo: string, dv: string): boolean {
        let soma = 0;
        let multiplicador = 2;

        for (let i = campo.length - 1; i >= 0; i--) {
            const digito = parseInt(campo.charAt(i));
            let resultado = digito * multiplicador;

            if (resultado > 9) {
                resultado = Math.floor(resultado / 10) + (resultado % 10);
            }

            soma += resultado;
            multiplicador = multiplicador === 2 ? 1 : 2;
        }

        const dvCalculado = ((10 - (soma % 10)) % 10).toString();
        return dvCalculado === dv;
    }
    
    /**
     * Calcula data de vencimento a partir do fator
     * Suporta dois ciclos:
     * - Ciclo 1: 07/10/1997 (fator 1000) até 21/02/2025 (fator 9999)
     * - Ciclo 2: 22/02/2025 (fator 1000) em diante
     */
    private calcularVencimento(fator: number): Date {
        const FATOR_BASE = 1000;
        const FATOR_MAXIMO_CICLO_1 = 9999;
        const DATA_BASE_CICLO_1 = new Date(1997, 9, 7);      // 07/10/1997
        const DATA_BASE_CICLO_2 = new Date(2025, 1, 22);     // 22/02/2025
        const DATA_TRANSICAO = new Date(2025, 1, 21);        // 21/02/2025
        
        if (fator > FATOR_MAXIMO_CICLO_1) {
            // Ajusta fator para o segundo ciclo
            fator = FATOR_BASE + (fator - FATOR_MAXIMO_CICLO_1);
        }

        const hoje = new Date();
        const diasDesdeFator = fator - FATOR_BASE;
        const vencimento: Date = new Date(hoje > DATA_TRANSICAO ? DATA_BASE_CICLO_2 : DATA_BASE_CICLO_1);

        vencimento.setDate(vencimento.getDate() + diasDesdeFator);
        return vencimento;
    }

    /**
     * Retorna nome do banco pelo código
     */
    private obterNomeBanco(codigo: string): string {
        const bancos: { [key: string]: string } = {
            '001': 'Banco do Brasil',
            '033': 'Santander',
            '104': 'Caixa Econômica Federal',
            '237': 'Bradesco',
            '341': 'Itaú',
            '356': 'Banco Real',
            '389': 'Banco Mercantil',
            '399': 'HSBC',
            '422': 'Banco Safra',
            '453': 'Banco Rural',
            '633': 'Banco Rendimento',
            '652': 'Itaú Unibanco',
            '745': 'Citibank'
        };

        return bancos[codigo] || `Banco ${codigo}`;
    }

    /**
     * Formata código de barras para exibição
     */
    public formatarCodigoBarras(codigo: string, tipo: 'bancario' | 'concessionaria'): string {
        const codigoLimpo = codigo.replace(/[\s.\-]/g, '');

        if (tipo === 'bancario' && codigoLimpo.length === 47) {
            // Formato: 00000.00000 00000.000000 00000.000000 0 00000000000000
            return `${codigoLimpo.substring(0, 5)}.${codigoLimpo.substring(5, 10)} ` +
                `${codigoLimpo.substring(10, 15)}.${codigoLimpo.substring(15, 21)} ` +
                `${codigoLimpo.substring(21, 26)}.${codigoLimpo.substring(26, 32)} ` +
                `${codigoLimpo.substring(32, 33)} ` +
                `${codigoLimpo.substring(33, 47)}`;
        } else if (tipo === 'concessionaria' && codigoLimpo.length === 48) {
            // Formato: 00000000000-0 00000000000-0 00000000000-0 00000000000-0
            return `${codigoLimpo.substring(0, 11)}-${codigoLimpo.substring(11, 12)} ` +
                `${codigoLimpo.substring(12, 23)}-${codigoLimpo.substring(23, 24)} ` +
                `${codigoLimpo.substring(24, 35)}-${codigoLimpo.substring(35, 36)} ` +
                `${codigoLimpo.substring(36, 47)}-${codigoLimpo.substring(47, 48)}`;
        }

        return codigo;
    }
}
