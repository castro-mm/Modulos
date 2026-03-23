import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { sharedConfig } from '@/shared/config/shared.config';
import { AuthService } from '@/secure/services/auth.service';
import { MessagesService } from '@/core/services/messages.service';
import { UserProfile } from '@/secure/models/user-profile.model';
import { ApiResponse } from '@/core/types/api-response.type';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { FieldValidationMessageComponent } from '@/core/components/field-validation-message.component';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { StatusCode } from '@/core/objects/enums';

@Component({
    selector: 'app-usuario-detail',
    imports: [...sharedConfig.imports, FieldValidationMessageComponent, ToggleButtonModule],
    templateUrl: './usuario-detail.component.html',
})
export class UsuarioDetailComponent implements OnInit {
    private authService = inject(AuthService);
    private messageService = inject(MessagesService);
    private fb = inject(FormBuilder);
    private dialogRef = inject(DynamicDialogRef);
    private dialogConfig = inject(DynamicDialogConfig);

    form: FormGroup;
    entity: UserProfile | null = null;
    isLoading = false;
    errorMessages: string[] = [];
    successMessage = '';
    roleOptions: { label: string; value: string }[] = [];

    constructor() {
        this.entity = this.dialogConfig.data?.item as UserProfile || null;

        this.form = this.fb.group({
            nomeCompleto: ['', [Validators.required, Validators.minLength(3)]],
            email: ['', [Validators.required, Validators.email]],
            role: ['', [Validators.required]],
            isActive: [true]
        });

        if (this.entity) {
            this.form.patchValue({
                nomeCompleto: this.entity.nomeCompleto,
                email: this.entity.email,
                role: this.entity.role,
                isActive: this.entity.isActive
            });
        }
    }

    async ngOnInit(): Promise<void> {
        await this.carregarRoles();
    }

    async carregarRoles(): Promise<void> {
        try {
            const response = await this.authService.getAllRoles();
            if (response.result?.isSuccessful) {
                const roles = response.result.data as { id: number; name: string }[];
                this.roleOptions = roles.map(r => ({ label: r.name, value: r.name }));
            }
        } catch (ex: any) {
            this.errorMessages = ['Erro ao carregar os perfis.'];
        }
    }

    async salvar(): Promise<void> {
        this.errorMessages = [];
        this.successMessage = '';

        if (this.form.invalid) return;

        this.isLoading = true;

        try {
            const data = this.form.value;
            const response = await this.authService.updateUser(Number(this.entity!.id), {
                nomeCompleto: data.nomeCompleto,
                email: data.email,
                role: data.role,
                isActive: data.isActive
            });

            if (response.result?.isSuccessful) {
                this.dialogRef.close(response);
            } else {
                const errors = response.result?.errors;
                this.errorMessages = errors?.length
                    ? errors.map(e => e.message)
                    : [response.result?.message || 'Erro ao salvar o usuário.'];
            }
        } catch (ex: any) {
            const errors = ex?.error?.result?.errors;
            this.errorMessages = errors?.length
                ? errors.map((e: any) => e.message)
                : [ex?.error?.result?.message || 'Erro ao salvar o usuário. Tente novamente.'];
        } finally {
            this.isLoading = false;
        }
    }

    resetarSenha(): void {
        if (!this.entity) return;

        this.messageService.confirm({
            header: 'Confirmação',
            message: `Deseja resetar a senha do usuário "${this.entity.nomeCompleto}"? Um e-mail será enviado com instruções para redefinição.`,
            accept: async () => {
                this.isLoading = true;
                this.errorMessages = [];
                this.successMessage = '';

                try {
                    const response = await this.authService.adminResetPassword(Number(this.entity!.id));
                    if (response.result?.isSuccessful) {
                        this.successMessage = response.result.data || 'E-mail de redefinição de senha enviado com sucesso.';
                    } else {
                        this.errorMessages = [response.result?.message || 'Erro ao resetar a senha.'];
                    }
                } catch (ex: any) {
                    this.errorMessages = [ex?.error?.result?.message || 'Erro ao resetar a senha. Tente novamente.'];
                } finally {
                    this.isLoading = false;
                }
            }
        });
    }

    excluir(): void {
        if (!this.entity) return;

        this.messageService.confirm({
            header: 'Confirmação',
            message: `Tem certeza que deseja excluir o usuário "${this.entity.nomeCompleto}"? Esta ação é irreversível.`,
            accept: async () => {
                this.isLoading = true;
                try {
                    const response = await this.authService.deleteUser(Number(this.entity!.id));
                    if (response.result?.isSuccessful) {
                        this.dialogRef.close(response);
                    } else {
                        this.errorMessages = [response.result?.message || 'Erro ao excluir o usuário.'];
                    }
                } catch (ex: any) {
                    this.errorMessages = [ex?.error?.result?.message || 'Erro ao excluir o usuário. Tente novamente.'];
                } finally {
                    this.isLoading = false;
                }
            }
        });
    }

    closeDialog(): void {
        this.dialogRef.close(null);
    }
}
