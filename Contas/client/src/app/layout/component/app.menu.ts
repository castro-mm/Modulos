import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MenuItem, MenuItemCommandEvent } from 'primeng/api';
import { AppMenuitem } from './app.menuitem';
import { AuthService } from '../../secure/services/auth.service';

@Component({
    selector: 'app-menu',
    imports: [CommonModule, AppMenuitem, RouterModule],
    template: `
        <ul class="layout-menu">
            @for (item of model; track $index) {
                <ng-container>
                    @if (!item.separator) {
                        <li app-menuitem [item]="item" [index]="$index" [root]="true"></li>
                    } @else {
                        <li class="menu-separator"></li>
                    }
                </ng-container>
            }
        </ul> 
    `
})
export class AppMenu {
    private authService = inject(AuthService);

    model: MenuItem[] = [];   

    ngOnInit() {
        this.model = [
            {
                label: 'Contas',
                items: [
                    { 
                        label: 'Dashboard', 
                        icon: 'pi pi-fw pi-building-columns', 
                        routerLink: ['contas'], 
                        routerLinkActiveOptions: { exact: true }, 
                        command: (event) => this.onMenuClick(event)
                    },
                    { 
                        label: 'Credor', 
                        icon: 'pi pi-fw pi-bars', 
                        routerLink: ['/contas/credor'], 
                        routerLinkActiveOptions: { exact: true }, 
                        command: (event) => this.onMenuClick(event)
                    },
                    { 
                        label: 'Segmentos do Credor', 
                        icon: 'pi pi-fw pi-bars', 
                        routerLink: ['/contas/segmento-do-credor'], 
                        routerLinkActiveOptions: { exact: true }, 
                        command: (event) => this.onMenuClick(event)
                    },
                    { 
                        label: 'Pagador', 
                        icon: 'pi pi-fw pi-bars', 
                        routerLink: ['/contas/pagador'], 
                        routerLinkActiveOptions: { exact: true }, 
                        command: (event) => this.onMenuClick(event)
                    },
                    { 
                        label: 'Registro da Conta', 
                        icon: 'pi pi-fw pi-dollar', 
                        routerLink: ['/contas/registro-da-conta'], 
                        routerLinkActiveOptions: { exact: true }, 
                        command: (event) => this.onMenuClick(event)
                    },
                ]
            }
        ];

        // Menu de Administração visível apenas para Admin
        if (this.authService.isAdmin()) {
            this.model.push({
                label: 'Administração',
                items: [
                    {
                        label: 'Usuários',
                        icon: 'pi pi-fw pi-users',
                        routerLink: ['/contas/usuarios'],
                        routerLinkActiveOptions: { exact: true },
                        command: (event) => this.onMenuClick(event)
                    },
                    {
                        label: 'Perfis',
                        icon: 'pi pi-fw pi-id-card',
                        routerLink: ['/contas/perfil'],
                        routerLinkActiveOptions: { exact: true },
                        command: (event) => this.onMenuClick(event)
                    }
                ]
            });
        }
    }

    private onMenuClick(event: MenuItemCommandEvent) {
        if (!event.item) return;
        
        const path = this.findMenuPath(event.item, this.model);
        const breadcrumbItems = path ? path.map(p => ({ label: p.label, routerLink: p.routerLink, icon: p.icon })) : [];

        const storedBreadcrumb = localStorage.getItem('breadcrumb');
        if (storedBreadcrumb) {
            localStorage.removeItem('breadcrumb');
        }

        localStorage.setItem('breadcrumb', JSON.stringify(breadcrumbItems));
    }

    private findMenuPath(item: MenuItem, items: MenuItem[], path: MenuItem[] = []): MenuItem[] | null {
        for (let menuItem of items) {
            if (menuItem === item) {
                return [...path, menuItem];
            }
            if (menuItem.items) {
                const foundPath = this.findMenuPath(item, menuItem.items, [...path, menuItem]);
                if (foundPath) {
                    return foundPath;
                }
            }
        }
        return null;
    }
}
