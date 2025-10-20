import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { sharedConfig } from '../config/shared.config';

@Component({
    selector: 'app-breadcrumb',
    imports: [...sharedConfig.imports],
    template: `
        <p-toolbar class="mb-4">
            <ng-template #start >
                <nav aria-label="breadcrumb" class="breadcrumb-container">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item">
                            <a><i [class]="home.icon"></i></a>
                        </li>
                        @for (item of items; track $index) {
                            <li class="breadcrumb-item" >
                                @if ($index !== items.length - 1 && item.routerLink) {
                                    <a [routerLink]="item.routerLink">
                                        @if (item.icon) {
                                            <i [class]="item.icon" style="margin-right: 0.5rem;"></i>
                                        }
                                        {{ item.label }}
                                    </a>
                                } @else {
                                    @if (item.icon) {
                                        <i [class]="item.icon" style="margin-right: 0.5rem;"></i>
                                    }
                                    {{ item.label }}
                                }
                            </li>
                        }
                    </ol>
                </nav>
            </ng-template>
        </p-toolbar>
    `,
    styles: `
        .breadcrumb-container {
            display: flex;
            align-items: center; /* Alinha verticalmente ao centro */
            height: 100%; /* Garante que o alinhamento seja relativo à altura do container */
        }
        .breadcrumb {
            margin-bottom: 0; /* Remove o espaçamento padrão */
        }    
    `
})
/**
 * @author Marcelo M. de Castro
 * @summary Componente de breadcrumb para navegação.
 * Fornece uma barra de navegação que exibe o caminho atual dentro da aplicação.
 * Os itens do breadcrumb são armazenados no localStorage para persistência entre recarregamentos de página.
 * @example 
 */
export class BreadcrumbComponent implements OnInit {
    home: MenuItem = { icon: 'pi pi-home', routerLink: '/' };
    items: MenuItem[] = [];

    constructor() { }

    ngOnInit(): void {
        const storedBreadcrumb = localStorage.getItem('breadcrumb');
        if (storedBreadcrumb) {
            this.items = JSON.parse(storedBreadcrumb);
        }
    }
}
