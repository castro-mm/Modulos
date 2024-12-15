import { Component, OnInit } from '@angular/core';
import { AccountService } from '../shared/services/account.service';
import { User } from '../shared/models/user';
import { JsonPipe } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { MessageService } from 'primeng/api';

@Component({
    selector: 'app-model-page',
    standalone: true,
    imports: [JsonPipe, ButtonModule, RippleModule],
    templateUrl: './model-page.component.html',
    styles: ``
})
export class ModelPageComponent implements OnInit {
    user: User = {} as User;
 
    constructor(private accountService: AccountService, private messageService: MessageService) { }

    ngOnInit(): void {
        this.user = this.accountService.currentUser() as User;
    }

    submit() {
        this.messageService.add({severity: 'success', summary: 'Teste', detail: 'Teste de mensagem'})
        console.log(this.user);
    }
}
