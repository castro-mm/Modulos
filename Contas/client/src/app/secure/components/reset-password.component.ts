import { Component, inject, ViewChild, AfterViewInit, viewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { RippleModule } from 'primeng/ripple';
import { MessageModule } from 'primeng/message';
import { FluidModule } from 'primeng/fluid';
import { AuthService } from '../services/auth.service';
import { AppFloatingConfigurator } from '../../layout/component/app.floatingconfigurator';
import { Password } from 'primeng/password';

@Component({
    selector: 'app-reset-password',
    standalone: true,
    imports: [CommonModule, FormsModule, RouterModule, ButtonModule, InputTextModule, PasswordModule, RippleModule, MessageModule, FluidModule, AppFloatingConfigurator],
    template: `
        <app-floating-configurator />
        <div class="bg-surface-50 dark:bg-surface-950 flex items-center justify-center min-h-screen min-w-screen overflow-hidden">
            <div class="flex flex-col items-center justify-center">
                <div style="border-radius: 56px; padding: 0.3rem; background: linear-gradient(180deg, var(--primary-color) 10%, rgba(33, 150, 243, 0) 30%)">
                    <div class="w-full bg-surface-0 dark:bg-surface-900 py-20 px-8 sm:px-20" style="border-radius: 53px">
                        <div class="text-center mb-8">
                            <div class="text-surface-900 dark:text-surface-0 text-3xl font-medium mb-4">Redefinir Senha</div>
                            <span class="text-muted-color font-medium">Informe sua nova senha</span>
                        </div>

                        @if (errorMessage) {
                            <p-message severity="error" [text]="errorMessage" styleClass="mb-4 w-full" />
                        }
                        @if (successMessage) {
                            <p-message severity="success" [text]="successMessage" styleClass="mb-4 w-full" />
                        }

                        <p-fluid>
                            <div class="flex flex-col gap-4">
                                <div class="flex flex-col gap-2">
                                    <label for="newPassword" class="text-surface-900 dark:text-surface-0 font-medium text-xl">Nova Senha</label>
                                    <p-password #newPasswordInput id="newPassword" [(ngModel)]="newPassword" placeholder="Digite a nova senha" [toggleMask]="true"></p-password>
                                </div>

                                <div class="flex flex-col gap-2">
                                    <label for="confirmPassword" class="text-surface-900 dark:text-surface-0 font-medium text-xl">Confirmar Senha</label>
                                    <p-password id="confirmPassword" [(ngModel)]="confirmPassword" placeholder="Confirme a nova senha" [toggleMask]="true" [feedback]="false" (keydown.enter)="onSubmit()"></p-password>
                                </div>

                                <p-button label="Redefinir Senha" styleClass="w-full" [loading]="loading" (onClick)="onSubmit()"></p-button>
                            </div>
                        </p-fluid>

                        <div class="text-center mt-6">
                            <a routerLink="/secure/login" class="font-medium text-primary cursor-pointer">Voltar para o login</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `
})
export class ResetPasswordComponent implements AfterViewInit {
    private authService = inject(AuthService);
    private router = inject(Router);
    private route = inject(ActivatedRoute);

    newPasswordInput = viewChild<Password>('newPasswordInput');

    newPassword = '';
    confirmPassword = '';
    loading = false;
    errorMessage = '';
    successMessage = '';

    private get email(): string {
        return this.route.snapshot.queryParams['email'] || '';
    }

    private get token(): string {
        return this.route.snapshot.queryParams['token'] || '';
    }

    ngAfterViewInit() {
        this.focusNewPassword();
    }

    async onSubmit() {
        this.errorMessage = '';
        this.successMessage = '';

        if (!this.newPassword || !this.confirmPassword) {
            this.errorMessage = 'Preencha todos os campos.';
            this.clearFieldsAndFocus();
            return;
        }

        if (this.newPassword !== this.confirmPassword) {
            this.errorMessage = 'As senhas não conferem.';
            this.clearFieldsAndFocus();
            return;
        }

        if (!this.email || !this.token) {
            this.errorMessage = 'Link de redefinição inválido ou expirado.';
            this.clearFieldsAndFocus();
            return;
        }

        this.loading = true;

        try {
            const response = await this.authService.resetPassword({
                email: this.email,
                token: this.token,
                newPassword: this.newPassword
            });

            if (response.result?.isSuccessful) {
                this.successMessage = 'Senha redefinida com sucesso! Redirecionando para o login...';
                this.clearFieldsAndFocus();
                setTimeout(() => this.router.navigate(['/secure/login']), 2000);
            } else {
                this.errorMessage = response.result?.message || 'Erro ao redefinir a senha.';
                this.clearFieldsAndFocus();
            }
        } catch (error: any) {
            this.errorMessage = error?.error?.result?.message || 'Erro ao redefinir a senha. Tente novamente.';
            this.clearFieldsAndFocus();
        } finally {
            this.loading = false;
        }
    }

    private focusNewPassword() {
        setTimeout(() => this.newPasswordInput()?.input?.nativeElement?.focus());
    }

    private clearFieldsAndFocus() {
        this.newPassword = '';
        this.confirmPassword = '';
        this.focusNewPassword();
    }
}
