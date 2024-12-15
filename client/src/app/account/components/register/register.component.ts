import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ToastModule } from 'primeng/toast';
import { AccountService } from '../../../shared/services/account.service';
import { Register } from '../../models/register';
import { Message } from 'primeng/api';
import { ValidationResult } from '../../../shared/models/validationResult';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { passwordCompareValidator } from '../../../shared/directives/password-compare.validator';
import { MessagesModule } from 'primeng/messages';
import { RippleModule } from 'primeng/ripple';
import { AutoFocusModule } from 'primeng/autofocus';

@Component({
    selector: 'app-register',
    standalone: true,
    imports: [
        PasswordModule, 
        CheckboxModule, 
        FormsModule, 
        InputTextModule, 
        ButtonModule, 
        ToastModule, 
        CommonModule, 
        RouterModule, 
        ReactiveFormsModule, 
        MessagesModule, 
        RippleModule, 
        AutoFocusModule
    ],
    templateUrl: './register.component.html',
})
export class RegisterComponent implements OnInit {
    form: FormGroup = new FormGroup({});
    model: Register = {} as Register;
    message: Message[] = [];
    strongPasswordRegx: RegExp = /^(?=[^A-Z]*[A-Z])(?=[^a-z]*[a-z])(?=\D*\d).{8,}$/;

    isLoading: boolean = false;

    constructor(private accountService: AccountService, private router: Router, private fb: FormBuilder, private ref: DynamicDialogRef) { }
    
    ngOnInit(): void {
        this.form = this.fb.group(
            {
                nome: ['', [Validators.required, Validators.minLength(8)]],
                email: ['', [Validators.required, Validators.email]],
                userName: ['', [Validators.required, Validators.minLength(5)]],
                password: ['', {validators: [Validators.required, Validators.pattern(this.strongPasswordRegx)]}],
                confirmPassword: ['', Validators.required]
            }, 
            { 
                validators: [ passwordCompareValidator ] 
            }
        );
    }

    register() {
        this.model = this.form.value as Register;
        this.isLoading = true;

        this.accountService.register(this.model)
            .subscribe({
                next: (result: ValidationResult) => {
                    if (result.statusCode === 200) {
                        this.message = [{ severity: 'success', summary: 'Cadastro realizado!', detail: 'Você será redirecionado para sua área em alguns segundos.' }];
                        this.isLoading = false;
                            setInterval(() =>{ 
                            this.router.navigateByUrl('secure/model-page');
                            this.ref.close()
                        }, 1500);                                            
                    }
                },
                error: (err: any) => {
                    const result: ValidationResult = err.error;
                    this.message = [{ severity: 'error', summary: 'Atenção', detail: result.message }];
                    this.isLoading = false;
                },                                
            }            
        )
    }

    close() {
        this.isLoading = false;
        this.ref.close();
    }
}