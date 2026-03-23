import { Component, inject, ViewChild, ElementRef, AfterViewInit, viewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { RippleModule } from 'primeng/ripple';
import { MessageModule } from 'primeng/message';
import { FluidModule } from 'primeng/fluid';
import { AuthService } from '../services/auth.service';
import { Login } from '../models/login.model';
import { AppFloatingConfigurator } from '../../layout/component/app.floatingconfigurator';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [CommonModule, FormsModule, RouterModule, ButtonModule, CheckboxModule, InputTextModule, PasswordModule, RippleModule, MessageModule, FluidModule, AppFloatingConfigurator],
    template: `
        <app-floating-configurator />
        <div class="bg-surface-50 dark:bg-surface-950 flex items-center justify-center min-h-screen min-w-screen overflow-hidden">
            <div class="flex flex-col items-center justify-center">
                <div style="border-radius: 56px; padding: 0.3rem; background: linear-gradient(180deg, var(--primary-color) 10%, rgba(33, 150, 243, 0) 30%)">
                    <div class="w-full bg-surface-0 dark:bg-surface-900 py-20 px-8 sm:px-20" style="border-radius: 53px">
                        <div class="text-center mb-8">
                            <div class="text-surface-900 dark:text-surface-0 text-3xl font-medium mb-4">Bem-vindo!</div>
                            <span class="text-muted-color font-medium">Entre com suas credenciais para continuar</span>
                        </div>

                        @for (error of errorMessages; track error) {
                            <p-message severity="error" [text]="error" styleClass="mb-4 w-full" />
                        }
                        <p-fluid>
                            <div class="flex flex-col gap-4">
                                <div class="flex flex-col gap-2">
                                    <label for="email" class="text-surface-900 dark:text-surface-0 text-xl font-medium">E-mail</label>
                                    <input #emailInput pInputText id="email" type="email" placeholder="Digite seu e-mail" [(ngModel)]="credentials.email" />
                                </div>

                                <div class="flex flex-col gap-2">
                                    <label for="password" class="text-surface-900 dark:text-surface-0 font-medium text-xl">Senha</label>
                                    <p-password id="password" [(ngModel)]="credentials.password" placeholder="Digite sua senha" [toggleMask]="true" [feedback]="false" (keydown.enter)="onLogin()"></p-password>
                                </div>

                                <div class="flex items-center justify-between gap-8">
                                    <div class="flex items-center">
                                        <p-checkbox [(ngModel)]="rememberMe" id="rememberme" binary class="mr-2"></p-checkbox>
                                        <label for="rememberme">Lembrar-me</label>
                                    </div>
                                    <a routerLink="/secure/forgot-password" class="font-medium no-underline ml-2 text-right cursor-pointer text-primary">Esqueceu a senha?</a>
                                </div>

                                <p-button label="Entrar" styleClass="w-full" [loading]="loading" (onClick)="onLogin()"></p-button>
                            </div>
                        </p-fluid>

                        <div class="text-center mt-6">
                            <span class="text-muted-color">Não tem uma conta? </span>
                            <a routerLink="/secure/register" class="font-medium text-primary cursor-pointer">Cadastre-se</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `
})
export class LoginComponent implements AfterViewInit {
    private authService = inject(AuthService);
    private router = inject(Router);
    private route = inject(ActivatedRoute);

    emailInput = viewChild<ElementRef<HTMLInputElement>>('emailInput');

    credentials: Login = { email: '', password: '' };
    rememberMe = false;
    loading = false;
    errorMessages: string[] = [];

    ngAfterViewInit() {
        this.focusEmail();
    }

    private focusEmail() {
        setTimeout(() => this.emailInput()?.nativeElement?.focus());
    }

    private clearFieldsAndFocus() {
        this.credentials = { email: '', password: '' };
        this.focusEmail();
    }

    async onLogin() {
        if (!this.credentials.email || !this.credentials.password) {
            this.errorMessages.push('Preencha todos os campos.');
            this.clearFieldsAndFocus();
            return;
        }

        this.loading = true;
        this.errorMessages = [];

        try {
            const response = await this.authService.login(this.credentials, this.rememberMe);

            if (response.result?.isSuccessful) {
                const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/contas';
                await this.router.navigateByUrl(returnUrl);
            } else {
                this.errorMessages.push(response.result?.message || 'Credenciais inválidas.');
                this.clearFieldsAndFocus();
            }
        } catch (ex: any) {
            const errors = ex?.error?.result?.errors;
            if (errors?.length) {
                this.errorMessages = errors.map((e: any) => e.message);
            } else {
                this.errorMessages = [ex?.error?.result?.message || 'Falha na autenticação. Tente novamente.'];
            }
            this.clearFieldsAndFocus();
        } finally {
            this.loading = false;
        }
    }
}
