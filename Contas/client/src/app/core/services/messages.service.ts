import { Injectable } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { StatusCode } from '../objects/enums';
import { ApiResponse } from '../models/api-response.model';

@Injectable({
    providedIn: 'root'
})
export class MessagesService {
    messageLifeTime: number = 3000;

    constructor(
        private messageService: MessageService, 
        private confirmationService: ConfirmationService
    ) { }

    showSuccess(summary: string, detail?: string) {
        this.messageService.add({ severity: 'success', summary, detail, life: this.messageLifeTime });
    }

    showInfo(summary: string, detail?: string) {
        this.messageService.add({ severity: 'info', summary, detail, life: this.messageLifeTime });
    }

    showWarn(summary: string, detail?: string) {
        this.messageService.add({ severity: 'warn', summary, detail, life: this.messageLifeTime });
    }

    showError(summary: string, detail?: string) {
        this.messageService.add({ severity: 'error', summary, detail, life: this.messageLifeTime });
    }

    clear() {
        this.messageService.clear();
    }
    
    confirm(options: { message: string; header?: string; icon?: string; accept: () => void; reject?: () => void; }) {
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

    showMessageFromReponse(response: ApiResponse, message?: string) { 
        switch (response.statusCode) {
            case StatusCode.OK:
            case StatusCode.NoContent:
                this.showSuccess('Requisição bem-sucedida.', response.message || message);
                break;
            case StatusCode.Created:
                this.showSuccess('Recurso criado com sucesso.', response.message || message);
                break;
            case StatusCode.BadRequest:
                this.showWarn('Requisição inválida.', response.message || 'Verifique os dados enviados e tente novamente.'); 
                break;
            case StatusCode.Unauthorized:
                this.showWarn('Acesso não autorizado.', response.message || 'Você não está autorizado a acessar este recurso.');
                break;
            case StatusCode.Forbidden:
                this.showWarn('Acesso proibido.', 'Você não tem permissão para acessar este recurso.');
                break;
            case StatusCode.NotFound:
                this.showWarn('Atenção.', response.message || 'Recurso não encontrado.');
                break;
            case StatusCode.InternalServerError:
                this.showError('Erro interno do servidor.', 'Ocorreu um erro no servidor. Tente novamente mais tarde.');
                break;
            case StatusCode.ServiceUnavailable:
                this.showError('Serviço indisponível.', 'O serviço está temporariamente indisponível. Tente novamente mais tarde.');
                break;
            default:
                break;
        }
    }
}
