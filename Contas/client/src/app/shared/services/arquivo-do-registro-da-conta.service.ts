import { inject, Injectable } from "@angular/core";
import { firstValueFrom } from "rxjs";
import { ArquivoDoRegistroDaConta } from "../models/arquivo-do-registro-da-conta.model";
import { EntityService } from "@/core/services/entity.service";
import { ApiResponse } from "@/core/types/api-response.type";
import { ArquivoService } from "./arquivo.service";
import { ModalidadeDeArquivo } from "../objects/enums";
import { SelectOption } from "@/core/types/select-option.type";

@Injectable({
    providedIn: 'root'
})
/**
 * @author Marcelo M. de Castro
 * @summary Serviço para gerenciar arquivos relacionados aos registros de conta.
 * @description Este serviço permite a manipulação de arquivos associados aos registros de conta, incluindo a obtenção de listas de arquivos e modalidades disponíveis.
 * @version 1.0.0
 */
export class ArquivoDoRegistroDaContaService extends EntityService<ArquivoDoRegistroDaConta> {
    arquivoService = inject(ArquivoService);

    /**
     * @summary Construtor da classe PagadorService.
     * @description Chama o construtor da classe base com o endpoint específico
     */
    constructor() { 
        super('arquivodoregistrodaconta');
    }

    /**
     * @summary Obtém a lista de arquivos relacionados a um registro de conta.
     * @param registroDaContaId ID do registro da conta.
     * @returns Promise<ApiResponse> contendo a lista de arquivos.
     */
    async getArquivosByRegistroDaContaId(registroDaContaId: number): Promise<ApiResponse> {
        var result$ = this.http.get<ApiResponse>(`${this.apiUrl}/get-by-registro-da-conta-id/${registroDaContaId}`);            
        return await firstValueFrom(result$);
    }

    /**
     * @summary Obtém as modalidades de arquivo disponíveis.
     * @returns SelectOption[] contendo as modalidades de arquivo.
     */
    getModalidadesDeArquivo(): SelectOption[] {
        return [
            { value: ModalidadeDeArquivo.BoletoBancario, label: 'Boleto Bancário', icon: 'pi pi-file-invoice' },
            { value: ModalidadeDeArquivo.ComprovanteDePagamento, label: 'Comprovante de Pagamento', icon: 'pi pi-receipt' }
        ];    
    }
}