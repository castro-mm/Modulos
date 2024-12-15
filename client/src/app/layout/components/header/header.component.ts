import { Component, ElementRef, ViewChild } from '@angular/core';
import { LayoutService } from '../../service/app.layout.service';
import { MenuItem, MessageService } from 'primeng/api';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../../shared/services/account.service';

@Component({
    selector: 'app-header',
    standalone: true,
    imports: [RouterLink, CommonModule],
    templateUrl: './header.component.html',
    styles: ``
})
export class HeaderComponent {
    items!: MenuItem[];

    @ViewChild('menubutton') menuButton!: ElementRef;
    @ViewChild('topbarmenubutton') topbarMenuButton!: ElementRef;
    @ViewChild('topbarmenu') menu!: ElementRef;

    constructor(public layoutService: LayoutService, private accountService: AccountService, private router: Router, private messageService: MessageService) { }

    logout() {
        this.accountService.logout();

        if (!this.accountService.currentUser()) {
            this.messageService.add({ severity: 'success', summary: 'Saída Confirmada', detail: 'Sua sessão foi encerrada.' });
            setTimeout(() => {
                this.router.navigateByUrl('/login');                
            }, 1500);
        } else {
            this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Houve uma falha ao realizar o Logoff. Tente novamente. Se o problema persistir, procure o administrador do sistema.' });
        }
    }
}
