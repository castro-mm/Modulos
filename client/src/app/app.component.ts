import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AccountService } from './shared/services/account.service';
import { User } from './shared/models/user';

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [ RouterOutlet ],
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {

    constructor(private accountService: AccountService) { }
    
    ngOnInit(): void {
        this.setCurrentUser();
    }

    setCurrentUser() {
        const user: User = JSON.parse(localStorage.getItem('user')!);        
        if (user) this.accountService.setCurrentUser(user);
    }
}
