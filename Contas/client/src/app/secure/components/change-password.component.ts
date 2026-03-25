import { Component, inject, ViewChild, ElementRef, AfterViewInit, viewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { PasswordModule } from 'primeng/password';
import { RippleModule } from 'primeng/ripple';
import { MessageModule } from 'primeng/message';
import { FluidModule } from 'primeng/fluid';
import { CardModule } from 'primeng/card';
import { AuthService } from '../services/auth.service';
import { Password } from 'primeng/password';

@Component({
    selector: 'app-change-password',
    standalone: true,
    imports: [CommonModule, FormsModule, RouterModule, ButtonModule, PasswordModule, RippleModule, MessageModule, FluidModule, CardModule],
    template: `
        <div class="flex items-center justify-center py-4 px-2 sm:py-8">
            <p-card header="Alterar Senha" [style]="{ 'min-width': '300px', 'max-width': '500px', 'width': '100%' }">
                <div class="text-center mb-6">
                    <span class="text-muted-color font-medium">Informe sua senha atual e a nova senha</span>
                </div>

                @for (error of errorMessages; track error) {
                    <p-message severity="error" [text]="error" styleClass="mb-2 w-full" />
                }
                @if (successMessage) {
                    <p-message severity="success" [text]="successMessage" styleClass="mb-4 w-full" />
                }

                <p-fluid>
                    <div class="flex flex-col gap-4">
                        <div class="flex flex-col gap-2">
                            <label for="currentPassword" class="font-medium">Senha Atual</label>
                            <p-password #currentPasswordInput id="currentPassword" [(ngModel)]="currentPassword" placeholder="Digite sua senha atual" [toggleMask]="true" [feedback]="false"></p-password>
                        </div>

                        <div class="flex flex-col gap-2">
                            <label for="newPassword" class="font-medium">Nova Senha</label>
                            <p-password id="newPassword" [(ngModel)]="newPassword" placeholder="Digite a nova senha" [toggleMask]="true"></p-password>
                        </div>

                        <div class="flex flex-col gap-2">
                            <label for="confirmPassword" class="font-medium">Confirmar Nova Senha</label>
                            <p-password id="confirmPassword" [(ngModel)]="confirmPassword" placeholder="Confirme a nova senha" [toggleMask]="true" [feedback]="false" (keydown.enter)="onSubmit()"></p-password>
                        </div>

                        <p-button label="Alterar Senha" styleClass="w-full" [loading]="loading" (onClick)="onSubmit()"></p-button>
                    </div>
                </p-fluid>
            </p-card>
        </div>
    `
})
export class ChangePasswordComponent implements AfterViewInit {
    private authService = inject(AuthService);
    private router = inject(Router);

    currentPasswordInput = viewChild<Password>('currentPasswordInput');

    currentPassword = '';
    newPassword = '';
    confirmPassword = '';
    loading = false;
    errorMessages: string[] = [];
    successMessage = '';

    ngAfterViewInit() {
        this.focusCurrentPassword();
    }

    private focusCurrentPassword() {
        setTimeout(() => this.currentPasswordInput()?.input?.nativeElement?.focus());
    }

    private clearFieldsAndFocus() {
        this.currentPassword = '';
        this.newPassword = '';
        this.confirmPassword = '';
        this.focusCurrentPassword();
    }

    async onSubmit() {
        this.errorMessages = [];
        this.successMessage = '';

        if (!this.currentPassword || !this.newPassword || !this.confirmPassword) {
            this.errorMessages = ['Preencha todos os campos.'];
            this.clearFieldsAndFocus();
            return;
        }

        if (this.newPassword !== this.confirmPassword) {
            this.errorMessages = ['As senhas não conferem.'];
            this.clearFieldsAndFocus();
            return;
        }

        this.loading = true;

        try {
            const response = await this.authService.changePassword({
                currentPassword: this.currentPassword,
                newPassword: this.newPassword
            });

            if (response.result?.isSuccessful) {
                this.successMessage = 'Senha alterada com sucesso!';
                this.clearFieldsAndFocus();
            } else {
                const errors = response.result?.errors;
                if (errors?.length) {
                    this.errorMessages = errors.map(e => e.message);
                } else {
                    this.errorMessages = [response.result?.message || 'Erro ao alterar a senha.'];
                }
                this.clearFieldsAndFocus();
            }
        } catch (ex: any) {
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
