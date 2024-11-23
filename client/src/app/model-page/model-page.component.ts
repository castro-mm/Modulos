import { Component, OnInit } from '@angular/core';
import { AccountService } from '../shared/services/account.service';
import { User } from '../shared/models/user';

@Component({
    selector: 'app-model-page',
    standalone: true,
    imports: [],
    templateUrl: './model-page.component.html',
    styles: ``
})
export class ModelPageComponent implements OnInit {
    user: User = {} as User;
    value: any = {};

    constructor(private accountService: AccountService) { }

    ngOnInit(): void {
        this.user = this.accountService.currentUser() as User;
        if (this.user) {
            this.value = JSON.stringify(this.user);
        } 
    }
}
