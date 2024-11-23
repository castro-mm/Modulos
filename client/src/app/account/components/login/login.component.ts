import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule} from '@angular/router';

import { PasswordModule } from 'primeng/password';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

import { LayoutService } from '../../../layout/service/app.layout.service';
import { AccountService } from '../../../shared/services/account.service';
import { ValidationResult } from '../../../shared/models/validationResult';

import { Auth } from '../../models/auth';
import { DialogModule } from 'primeng/dialog';
import { RegisterComponent } from "../register/register.component";
import { ModalService } from '../../../shared/services/modal.service';
import { DialogService } from 'primeng/dynamicdialog';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [PasswordModule, CheckboxModule, FormsModule, InputTextModule, ButtonModule, ToastModule, CommonModule, RouterModule, DialogModule],
    templateUrl: './login.component.html',
    providers: [MessageService, ModalService, DialogService]
})
export class LoginComponent implements AfterViewInit {
    @ViewChild('form', { static: false }) form?: NgForm
    auth: Auth = {} as Auth;
    formSubmitted: boolean = false;
    dialogVisible: boolean = false;
    modalComponent = RegisterComponent;

    constructor(public layoutService: LayoutService, private accountService: AccountService, private router: Router, private actvatedRoute: ActivatedRoute, private messageService: MessageService, private modalService: ModalService) { }

    ngAfterViewInit(): void {
        this.actvatedRoute.queryParams.subscribe({
            next: (params) => {
                if (params['accessDenied']) {
                    this.messageService.add({ severity: 'error', summary: 'Acesso Negado', detail: 'Sessão expirada! Entre novamente.' });
                }
                if (params['logout']) {
                    this.messageService.add({ severity: 'success', summary: 'Saída Confirmada', detail: 'Sua sessão foi encerrada.' });
                }
            }
        })
    }

    login() {        
        this.formSubmitted = true;
        
        if(this.isValidForm) {
            this.accountService.login(this.auth).subscribe({
                next: () => {
                    if (this.accountService.currentUser()) {
                        this.router.navigateByUrl('secure/model-page');
                    }
                },
                error: (e) => {
                    const result: ValidationResult = e.error;
                    this.messageService.add({ severity: 'error', summary: 'Atenção', detail: result.message });
                    this.resetForm()
                }
            });    
        }
    }

    private resetForm() {
        this.form!.resetForm();
        this.formSubmitted = false;
    }

    get isValidForm() {
        return this.auth.userName && this.auth.password;
    }

    showRegister() {
        this.modalService.show(this.modalComponent, 'Cadastro de Usuário', 600)
    }
}
