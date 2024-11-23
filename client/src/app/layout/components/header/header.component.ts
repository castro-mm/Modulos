import { Component, ElementRef, ViewChild } from '@angular/core';
import { LayoutService } from '../../service/app.layout.service';
import { MenuItem } from 'primeng/api';
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

    constructor(public layoutService: LayoutService, private accountService: AccountService, private router: Router) { }

    logout() {
        this.accountService.logout();
        this.router.navigate(['/login'], {queryParams: {logout: 'ok'}});
    }
}
