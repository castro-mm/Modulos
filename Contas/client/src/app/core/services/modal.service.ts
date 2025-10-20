import { Component, Injectable, Type } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Observable } from 'rxjs';

/**
 * @author Marcelo M. de Castro
 * @summary Serviço para abrir diálogos modais.
 * Utiliza o DialogService do PrimeNG para gerenciar a abertura e fechamento dos diálogos.
 * Permite a passagem de componentes dinâmicos, dados e configurações personalizadas.
 */
@Injectable({
    providedIn: 'root'
})
export class ModalService {
    dialogRef: DynamicDialogRef | null = null;

    constructor(private dialogService: DialogService) { }    

    /**
     * @summary Abre um diálogo modal com o componente especificado.
     * @param component O componente a ser exibido no modal. 
     * @param header O título do modal. 
     * @param data Os dados a serem passados para o componente do modal. 
     * @param width A largura do modal (padrão é '20%'). 
     * @returns Observable<any> que emite o resultado ao fechar o diálogo. 
     * @example
     * const onClose = this.modalService.openDialogPage(
     *   MyModalComponent as Type<Component>,
     *   'Título do Modal',
     *   { data: 'dados para o modal' },
     *   '50%'
     * );
     * 
     * if (onClose) {
     *   onClose.subscribe(result => {
     *     console.log('Modal fechado com resultado:', result);
     *   });
     * }
     */
    openDialogPage(
        component: Type<Component>, 
        header: string, 
        data: any, 
        width: string = '20%'
    ) : Observable<any>  {
        this.dialogRef = this.dialogService.open(component, {
            header: header,
            width: width,
            modal: true,
            data: data,
        });

        return this.dialogRef.onClose;
    }
}
