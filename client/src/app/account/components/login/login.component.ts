import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterModule} from '@angular/router';

import { PasswordModule } from 'primeng/password';
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
    imports: [
        ReactiveFormsModule,
        PasswordModule, 
        FormsModule, 
        InputTextModule, 
        ButtonModule, 
        ToastModule, 
        CommonModule, 
        RouterModule, 
        DialogModule
    ],
    templateUrl: './login.component.html',
    providers: [MessageService, ModalService, DialogService]
})
export class LoginComponent implements OnInit {
    form: FormGroup = new FormGroup({});
    model: Auth = {} as Auth;
    modalComponent = RegisterComponent;

    isLoading: boolean = false;
    dialogVisible: boolean = false;    

    constructor(public layoutService: LayoutService, private accountService: AccountService, private router: Router, private messageService: MessageService, private modalService: ModalService, private fb: FormBuilder) {
        if (this.accountService.currentUser()) {
            this.router.navigateByUrl('secure/model-page');
        }
    }

    ngOnInit(): void {
        this.form = this.fb.group(
            {
                userName: ['', [ Validators.required ]],
                password: ['', [ Validators.required ]]
            }
        )
    }

    login() {
        this.model = this.form.value as Auth;
        this.isLoading = true;        

        if(this.model) {
            this.accountService.login(this.model).subscribe({
                next: () => {
                    setInterval(() => {
                        if (!this.accountService.currentUser()) {
                            this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'Houve um erro na autenticação. Se o problema persistir, entre em contato com o suporte.' });
                            return;
                        } 
                        this.router.navigateByUrl('secure/model-page');
                        this.isLoading = false;
                    }, 1500);                
                },
                error: (e) => {
                    const result: ValidationResult = e.error;
                    this.messageService.add({ severity: 'error', summary: 'Erro', detail: result.message });
                    this.isLoading = false;
                }
            });    
        }
    }

    showRegisterDialog() { 
        this.modalService.show(this.modalComponent, 'Cadastro de Usuário', 60).subscribe({
            next: (result: ValidationResult) => { // when "this.ref.close(this.model);" is called from modal dialog page, this method is invoked.
            }
        })
    }
}
