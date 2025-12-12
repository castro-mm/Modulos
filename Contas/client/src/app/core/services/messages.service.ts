import { Injectable } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { StatusCode } from '../objects/enums';
import { ApiResponse } from '../types/api-response.type';

@Injectable({
    providedIn: 'root'
})
/**
 * @author Marcelo M. de Castro
 * @summary Serviço para exibição de mensagens e confirmações.
 * @description Este serviço encapsula a funcionalidade de exibição de mensagens de sucesso, informação, aviso e erro, bem como diálogos de confirmação, utilizando os serviços do PrimeNG.
 * @version 1.0.0
 */
export class MessagesService {
    messageLifeTime: number = 3000;

    /**
     * @summary Construtor do serviço MessagesService.
     * @description Injeta os serviços MessageService e ConfirmationService do PrimeNG para uso na exibição de mensagens e diálogos de confirmação.
     * @param messageService Serviço para exibição de mensagens. 
     * @param confirmationService Serviço para exibição de diálogos de confirmação.
     */
    constructor(
        private messageService: MessageService, 
        private confirmationService: ConfirmationService
    ) { }

    /**
     * @summary Exibe uma mensagem de sucesso.
     * @description Adiciona uma mensagem de sucesso ao serviço de mensagens com um resumo e detalhe opcionais.
     * @param detail Detalhe da mensagem a ser exibida.
     * @param summary Resumo da mensagem a ser exibida.
     * @example
     * this.messagesService.showSuccess('Operação concluída com sucesso.', 'Sucesso');
     */
    showSuccess(detail?: string, summary: string = 'Requisição bem-sucedida.') {
        this.messageService.add({ severity: 'success', summary, detail, life: this.messageLifeTime });
    }

    /**
     * @summary Exibe uma mensagem de informação.
     * @description Adiciona uma mensagem de informação ao serviço de mensagens com um resumo e detalhe opcionais.
     * @param detail Detalhe da mensagem a ser exibida.
     * @param summary Resumo da mensagem a ser exibida.
     * @example
     * this.messagesService.showInfo('Esta é uma mensagem informativa.', 'Informação');
     */
    showInfo(detail?: string, summary: string = 'Informação') {
        this.messageService.add({ severity: 'info', summary, detail, life: this.messageLifeTime });
    }

    /**
     * @summary Exibe uma mensagem de aviso.
     * @description Adiciona uma mensagem de aviso ao serviço de mensagens com um resumo e detalhe opcionais.
     * @param detail Detalhe da mensagem a ser exibida.
     * @param summary Resumo da mensagem a ser exibida.
     * @example
     * this.messagesService.showWarn('Este é um aviso importante.', 'Aviso');
     */
    showWarn(detail?: string, summary: string = 'Aviso') {
        this.messageService.add({ severity: 'warn', summary, detail, life: this.messageLifeTime });
    }

    /**
     * @summary Exibe uma mensagem de erro.
     * @description Adiciona uma mensagem de erro ao serviço de mensagens com um resumo e detalhe opcionais.
     * @param detail Detalhe da mensagem a ser exibida.
     * @param summary Resumo da mensagem a ser exibida.
     * @example
     * this.messagesService.showError('Ocorreu um erro durante a operação.', 'Erro');
     */
    showError(detail?: string, summary: string = 'Erro') {
        this.messageService.add({ severity: 'error', summary, detail, life: this.messageLifeTime });
    }

    /**
     * @summary Limpa as mensagens emitidas.
     * @description Remove todas as mensagens atualmente exibidas pelo serviço de mensagens.
     */
    clear() {
        this.messageService.clear();
    }
    
    /**
     * @summary Exibe um diálogo de confirmação.
     * @description Utiliza o serviço de confirmação para exibir um diálogo com mensagem, cabeçalho, ícone e callbacks para aceitação e rejeição.
     * @param message A mensagem a ser exibida no diálogo de confirmação.
     * @param header O cabeçalho do diálogo de confirmação (opcional).
     * @param icon O ícone do diálogo de confirmação (opcional).
     * @param accept Callback a ser executado quando o usuário aceitar a confirmação.
     * @param reject Callback a ser executado quando o usuário rejeitar a confirmação (opcional).
     * @example 
     * this.messagesService.confirm({
     *     message: 'Você tem certeza que deseja continuar?',
     *     accept: () => { /* lógica de aceitação *\/ },
     *     reject: () => { /* lógica de rejeição *\/ }
     * });
     * @returns {void} Nenhum valor retornado.
     */
    confirm(options: { message: string; header?: string; icon?: string; accept: () => void; reject?: () => void; }): void {
        this.confirmationService.confirm({
            message: options.message,
            header: options.header || 'Confirmação',
            icon: options.icon || 'pi pi-exclamation-triangle',
            acceptButtonProps: { 
                label: 'Sim', 
                icon: 'pi pi-check', 
                severity: 'success', 
                class: 'p-button-text' 
            },
            rejectButtonProps: { 
                label: 'Não', 
                icon: 'pi pi-times', 
                severity: 'secondary', 
                outlined: true, 
                class: 'p-button-text'
            },
            accept: options.accept,
            reject: options.reject
        });
    }

    /**
     * @summary Exibe uma mensagem baseada na resposta da API.
     * @description Analisa o código de status da resposta da API e exibe uma mensagem apropriada utilizando os métodos de exibição de mensagens.
     * @param response A resposta da API a ser analisada.
     * @param message Uma mensagem adicional a ser exibida (opcional).
     * @example
     * this.messagesService.showMessageFromReponse(apiResponse, 'Operação concluída.');
     * @returns {void} Nenhum valor retornado.
     */
    showMessageFromReponse(response: ApiResponse, message?: string): void { 
        switch (response.statusCode) {
            case StatusCode.OK:
            case StatusCode.NoContent:
                this.showSuccess(response.message || message, 'Requisição bem-sucedida.');
                break;
            case StatusCode.Created:
                this.showSuccess(response.message || message, 'Recurso criado com sucesso.');
                break;
            case StatusCode.BadRequest:
                this.showWarn(response.message || 'Verifique os dados enviados e tente novamente.', 'Requisição inválida.'); 
                break;
            case StatusCode.Unauthorized:
                this.showWarn(response.message || 'Você não está autorizado a acessar este recurso.', 'Acesso não autorizado.');
                break;
            case StatusCode.Forbidden:
                this.showWarn('Você não tem permissão para acessar este recurso.', 'Acesso proibido.');
                break;
            case StatusCode.NotFound:
                this.showWarn(response.message || 'Recurso não encontrado.');
                break;
            case StatusCode.InternalServerError:
                this.showError('Ocorreu um erro no servidor. Tente novamente mais tarde.', 'Erro interno do servidor.');
                break;
            case StatusCode.ServiceUnavailable:
                this.showError('O serviço está temporariamente indisponível. Tente novamente mais tarde.', 'Serviço indisponível.');
                break;
            default:
                break;
        }
    }
}
