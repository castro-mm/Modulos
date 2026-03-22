import { Component, inject, ViewChild, ElementRef, AfterViewInit, viewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { RippleModule } from 'primeng/ripple';
import { MessageModule } from 'primeng/message';
import { FluidModule } from 'primeng/fluid';
import { AuthService } from '../services/auth.service';
import { Register } from '../models/register.model';
import { AppFloatingConfigurator } from '../../layout/component/app.floatingconfigurator';

@Component({
    selector: 'app-register',
    standalone: true,
    imports: [CommonModule, FormsModule, RouterModule, ButtonModule, InputTextModule, PasswordModule, RippleModule, MessageModule, FluidModule, AppFloatingConfigurator],
    template: `
        <app-floating-configurator />
        <div class="bg-surface-50 dark:bg-surface-950 flex items-center justify-center min-h-screen min-w-screen overflow-hidden">
            <div class="flex flex-col items-center justify-center">
                <div style="border-radius: 56px; padding: 0.3rem; background: linear-gradient(180deg, var(--primary-color) 10%, rgba(33, 150, 243, 0) 30%)">
                    <div class="w-full bg-surface-0 dark:bg-surface-900 py-20 px-8 sm:px-20" style="border-radius: 53px">
                        <div class="text-center mb-8">
                            <div class="text-surface-900 dark:text-surface-0 text-3xl font-medium mb-4">Criar Conta</div>
                            <span class="text-muted-color font-medium">Preencha os dados abaixo para se cadastrar</span>
                        </div>

                        @if (errorMessages.length > 0) {
                            @for (error of errorMessages; track $index) {
                                <p-message severity="error" [text]="error" styleClass="mb-4 w-full" />
                            }
                        }
                        @if (successMessage) {
                            <p-message severity="success" [text]="successMessage" styleClass="mb-4 w-full" />
                        }

                        <p-fluid>
                            <div class="flex flex-col gap-4">
                                <div class="flex flex-col gap-2">
                                    <label for="nomeCompleto" class="text-surface-900 dark:text-surface-0 text-xl font-medium">Nome Completo</label>
                                    <input #nomeCompletoInput pInputText id="nomeCompleto" type="text" placeholder="Digite seu nome completo" [(ngModel)]="user.nomeCompleto" />
                                </div>

                                <div class="flex flex-col gap-2">
                                    <label for="email" class="text-surface-900 dark:text-surface-0 text-xl font-medium">E-mail</label>
                                    <input pInputText id="email" type="email" placeholder="Digite seu e-mail" [(ngModel)]="user.email" />
                                </div>

                                <div class="flex flex-col gap-2">
                                    <label for="password" class="text-surface-900 dark:text-surface-0 font-medium text-xl">Senha</label>
                                    <p-password id="password" [(ngModel)]="user.password" placeholder="Digite sua senha" [toggleMask]="true"></p-password>
                                </div>

                                <div class="flex flex-col gap-2">
                                    <label for="confirmPassword" class="text-surface-900 dark:text-surface-0 font-medium text-xl">Confirmar Senha</label>
                                    <p-password id="confirmPassword" [(ngModel)]="confirmPassword" placeholder="Confirme sua senha" [toggleMask]="true" [feedback]="false" (keydown.enter)="onRegister()"></p-password>
                                </div>

                                <p-button label="Cadastrar" styleClass="w-full" [loading]="loading" (onClick)="onRegister()"></p-button>
                            </div>
                        </p-fluid>

                        <div class="text-center mt-6">
                            <span class="text-muted-color">Já tem uma conta? </span>
                            <a routerLink="/secure/login" class="font-medium text-primary cursor-pointer">Entrar</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `
})
export class RegisterComponent implements AfterViewInit {
    private authService = inject(AuthService);
    private router = inject(Router);

    nomeCompletoInput = viewChild<ElementRef<HTMLInputElement>>('nomeCompletoInput');

    user: Register = { email: '', password: '', nomeCompleto: '' };
    confirmPassword = '';
    loading = false;
    errorMessages: string[] = [];
    successMessage = '';

    ngAfterViewInit() {
        this.focusNomeCompleto();
    }

    private focusNomeCompleto() {
        setTimeout(() => this.nomeCompletoInput()?.nativeElement?.focus());
    }

    private clearFieldsAndFocus() {
        this.user = { email: '', password: '', nomeCompleto: '' };
        this.confirmPassword = '';
        this.focusNomeCompleto();
    }

    async onRegister() {
        this.errorMessages = [];
        this.successMessage = '';

        if (!this.user.nomeCompleto || !this.user.email || !this.user.password) {
            this.errorMessages.push('Preencha todos os campos.');
            this.clearFieldsAndFocus();
            return;
        }

        if (this.user.password !== this.confirmPassword) {
            this.errorMessages.push('As senhas não conferem.');
            this.clearFieldsAndFocus();
            return;
        }

        this.loading = true;

        try {
            const response = await this.authService.register(this.user);

            if (response.result?.isSuccessful) {
                this.successMessage = response.result?.message || 'Conta criada com sucesso! Verifique seu e-mail para ativá-la.';
                this.clearFieldsAndFocus();
            } else {
                this.errorMessages.push(response.result?.message || 'Erro ao criar a conta.');
                this.clearFieldsAndFocus();
            }
        } catch (ex: any) {
            const errors = ex?.error?.result?.errors;
            this.errorMessages = errors.length 
                ? errors.map((e: any) => e.message) 
                : [ex?.error?.result?.message || 'Erro ao criar a conta. Tente novamente.'];
            this.clearFieldsAndFocus();
        } finally {
            this.loading = false;
        }
    }
}


