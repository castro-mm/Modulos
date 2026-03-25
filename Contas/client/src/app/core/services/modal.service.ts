import { Component, Injectable, Type } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
/**
 * @author Marcelo M. de Castro
 * @summary Serviço para abrir diálogos modais.
 * Utiliza o DialogService do PrimeNG para gerenciar a abertura e fechamento dos diálogos.
 * Permite a passagem de componentes dinâmicos, dados e configurações personalizadas.
 * @version 1.1.0
 */
export class ModalService {
    dialogRef: DynamicDialogRef | null = null;

    constructor(private dialogService: DialogService) { }    

    /**
     * @summary Abre um diálogo modal com o componente especificado.
     * @param component O componente a ser exibido no modal. 
     * @param header O título do modal. 
     * @param data Os dados a serem passados para o componente do modal. 
     * @param width A largura do modal (padrão é '20%'). Será adaptada automaticamente em telas menores.
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
        const responsiveWidth = this.getResponsiveWidth(width);

        this.dialogRef = this.dialogService.open(component, {
            header: header,
            width: responsiveWidth,
            modal: true,
            data: data,
            closable: true,
            breakpoints: {
                '575px': '95vw',
                '768px': '90vw',
                '992px': '85vw',
                '1200px': '75vw'
            },
            maximizable: this.isMobileDevice(),
        });

        return this.dialogRef.onClose;
    }

    /**
     * @summary Calcula a largura responsiva do modal baseado no tamanho da tela.
     * @param desktopWidth Largura desejada para desktop.
     * @returns Largura adaptada para o breakpoint atual.
     */
    private getResponsiveWidth(desktopWidth: string): string {
        const screenWidth = window.innerWidth;

        if (screenWidth < 576) {
            return '95vw';
        } else if (screenWidth < 768) {
            return '90vw';
        } else if (screenWidth < 992) {
            return '85vw';
        } else if (screenWidth < 1200) {
            return '75vw';
        }

        return desktopWidth;
    }

    /**
     * @summary Verifica se o dispositivo é mobile ou tablet (touch device).
     * @returns true se for um dispositivo touch.
     */
    private isMobileDevice(): boolean {
        return window.innerWidth < 992 || ('ontouchstart' in window);
    }
}
