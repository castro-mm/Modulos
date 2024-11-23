import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ToastModule } from 'primeng/toast';
import { AccountService } from '../../../shared/services/account.service';
import { Register } from '../../models/register';

@Component({
    selector: 'app-register',
    standalone: true,
    imports: [PasswordModule, CheckboxModule, FormsModule, InputTextModule, ButtonModule, ToastModule, CommonModule, RouterModule],
    templateUrl: './register.component.html',
})
export class RegisterComponent{
    register: Register = {} as Register;
    formSubmitted: boolean = false;

    constructor(private accountService: AccountService) { }

    cadastrar() {

    }

    verificarDisponibilidadeDeUsuario() {

    }
}
