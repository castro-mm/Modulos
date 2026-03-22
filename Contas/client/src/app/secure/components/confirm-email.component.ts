import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { AuthService } from '../services/auth.service';
import { AppFloatingConfigurator } from '../../layout/component/app.floatingconfigurator';

@Component({
    selector: 'app-confirm-email',
    standalone: true,
    imports: [CommonModule, RouterModule, ButtonModule, MessageModule, ProgressSpinnerModule, AppFloatingConfigurator],
    template: `
        <app-floating-configurator />
        <div class="bg-surface-50 dark:bg-surface-950 flex items-center justify-center min-h-screen min-w-screen overflow-hidden">
            <div class="flex flex-col items-center justify-center">
                <div style="border-radius: 56px; padding: 0.3rem; background: linear-gradient(180deg, var(--primary-color) 10%, rgba(33, 150, 243, 0) 30%)">
                    <div class="w-full bg-surface-0 dark:bg-surface-900 py-20 px-8 sm:px-20" style="border-radius: 53px">
                        <div class="text-center mb-8">
                            <div class="text-surface-900 dark:text-surface-0 text-3xl font-medium mb-4">Confirmação de E-mail</div>
                        </div>

                        @if (loading) {
                            <div class="flex justify-center">
                                <p-progressSpinner strokeWidth="4" />
                            </div>
                            <p class="text-center text-muted-color mt-4">Confirmando seu e-mail...</p>
                        }

                        @if (errorMessage) {
                            <p-message severity="error" [text]="errorMessage" styleClass="mb-4 w-full" />
                        }

                        @if (successMessage) {
                            <p-message severity="success" [text]="successMessage" styleClass="mb-4 w-full" />
                        }

                        @if (!loading) {
                            <div class="text-center mt-6">
                                <a routerLink="/secure/login" class="font-medium text-primary cursor-pointer">Ir para o login</a>
                            </div>
                        }
                    </div>
                </div>
            </div>
        </div>
    `
})
export class ConfirmEmailComponent implements OnInit {
    private authService = inject(AuthService);
    private route = inject(ActivatedRoute);

    loading = true;
    errorMessage = '';
    successMessage = '';

    async ngOnInit() {
        const email = this.route.snapshot.queryParams['email'] || '';
        const token = this.route.snapshot.queryParams['token'] || '';

        if (!email || !token) {
            this.errorMessage = 'Link de confirmação inválido.';
            this.loading = false;
            return;
        }

        try {
            const response = await this.authService.confirmEmail(email, token);

            if (response.result?.isSuccessful) {
                this.successMessage = 'E-mail confirmado com sucesso! Você já pode fazer login.';
            } else {
                this.errorMessage = response.result?.message || 'Erro ao confirmar o e-mail.';
            }
        } catch (error: any) {
            const errors = error?.error?.result?.errors;
            if (errors?.length) {
                this.errorMessage = errors.map((e: any) => e.message).join('. ');
            } else {
                this.errorMessage = error?.error?.result?.message || 'Erro ao confirmar o e-mail. Tente novamente.';
            }
        } finally {
            this.loading = false;
        }
    }
}
