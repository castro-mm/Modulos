import { Component, inject, ViewChild, ElementRef, AfterViewInit, viewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { RippleModule } from 'primeng/ripple';
import { MessageModule } from 'primeng/message';
import { FluidModule } from 'primeng/fluid';
import { AuthService } from '../services/auth.service';
import { AppFloatingConfigurator } from '../../layout/component/app.floatingconfigurator';

@Component({
    selector: 'app-forgot-password',
    standalone: true,
    imports: [CommonModule, FormsModule, RouterModule, ButtonModule, InputTextModule, RippleModule, MessageModule, FluidModule, AppFloatingConfigurator],
    template: `
        <app-floating-configurator />
        <div class="bg-surface-50 dark:bg-surface-950 flex items-center justify-center min-h-screen min-w-screen overflow-hidden">
            <div class="flex flex-col items-center justify-center">
                <div style="border-radius: 56px; padding: 0.3rem; background: linear-gradient(180deg, var(--primary-color) 10%, rgba(33, 150, 243, 0) 30%)">
                    <div class="w-full bg-surface-0 dark:bg-surface-900 py-20 px-8 sm:px-20" style="border-radius: 53px">
                        <div class="text-center mb-8">
                            <div class="text-surface-900 dark:text-surface-0 text-3xl font-medium mb-4">Esqueceu a Senha?</div>
                            <span class="text-muted-color font-medium">Informe seu e-mail para receber as instruções de recuperação</span>
                        </div>
                        
                        @if(errorMessages.length > 0) {
                            @for (error of errorMessages; track error) {
                                <p-message severity="error" [text]="error" styleClass="mb-4 w-full" />
                            }
                        }
                        @if (successMessage) {
                            <p-message severity="success" [text]="successMessage" styleClass="mb-4 w-full" />
                        }

                        <p-fluid>
                            <div class="flex flex-col gap-4">
                                <div class="flex flex-col gap-2">
                                    <label for="email" class="text-surface-900 dark:text-surface-0 text-xl font-medium">E-mail</label>
                                    <input #emailInput pInputText id="email" type="email" placeholder="Digite seu e-mail" [(ngModel)]="email" (keydown.enter)="onSubmit()" />
                                </div>

                                <p-button label="Enviar" styleClass="w-full" [loading]="loading" (onClick)="onSubmit()"></p-button>
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
export class ForgotPasswordComponent implements AfterViewInit {
    private authService = inject(AuthService);

    emailInput = viewChild<ElementRef<HTMLInputElement>>('emailInput');

    email = '';
    loading = false;
    errorMessages: string[] = [];
    successMessage = '';

    ngAfterViewInit() {
        this.focusEmail();
    }

    private focusEmail() {
        setTimeout(() => this.emailInput()?.nativeElement?.focus());
    }

    private clearFieldsAndFocus() {
        this.email = '';
        this.focusEmail();
    }

    async onSubmit() {
        this.errorMessages = [];
        this.successMessage = '';

        if (!this.email) {
            this.errorMessages.push('Informe o e-mail.');
            this.clearFieldsAndFocus();
            return;
        }

        this.loading = true;

        try {
            const response = await this.authService.forgotPassword({ email: this.email });

            if (response.result?.isSuccessful) {
                this.successMessage = response.result.data || 'Instruções de recuperação enviadas para o seu e-mail.';
            } else {
                this.errorMessages.push(response.result?.message || 'Erro ao enviar o e-mail de recuperação.');
                this.clearFieldsAndFocus();
            }
        } catch (ex: any) {
            console.log(ex);
            const errors = ex?.error?.result?.errors;
            if (errors?.length) {
                this.errorMessages = errors.map((e: any) => e.message);
            } else {
                this.errorMessages = [ex?.error?.result?.message || 'Erro ao alterar a senha. Tente novamente.'];
            }
            this.clearFieldsAndFocus();
        } finally {
            this.loading = false;
        }
    }
}
