import { Component, inject, OnInit, signal, Type } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { sharedConfig } from '@/shared/config/shared.config';
import { BreadcrumbComponent } from '@/core/components/breadcrumb.component';
import { AuthService } from '@/secure/services/auth.service';
import { MessagesService } from '@/core/services/messages.service';
import { ModalService } from '@/core/services/modal.service';
import { UserProfile } from '@/secure/models/user-profile.model';
import { ApiResponse } from '@/core/types/api-response.type';
import { UsuarioDetailComponent } from './detail/usuario-detail.component';
import { TagModule } from 'primeng/tag';

@Component({
    selector: 'app-usuario',
    imports: [...sharedConfig.imports, BreadcrumbComponent, TagModule],
    templateUrl: './usuario.component.html',
})
export class UsuarioComponent implements OnInit {
    private authService = inject(AuthService);
    private messageService = inject(MessagesService);
    private modalService = inject(ModalService);
    private fb = inject(FormBuilder);

    form: FormGroup;
    usuarios = signal<UserProfile[]>([]);
    isLoading = false;

    constructor() {
        this.form = this.fb.group({
            nomeCompleto: [''],
            email: ['']
        });
    }

    ngOnInit(): void {
        this.listar();
    }

    async listar(): Promise<void> {
        this.isLoading = true;
        this.usuarios.set([]);

        try {
            const response = await this.authService.getAllUsers();
            if (response.result?.isSuccessful) {
                let users = response.result.data as UserProfile[];

                // Filtros client-side
                const nome = this.form.value.nomeCompleto?.toLowerCase() || '';
                const email = this.form.value.email?.toLowerCase() || '';

                if (nome) {
                    users = users.filter(u => u.nomeCompleto.toLowerCase().includes(nome));
                }
                if (email) {
                    users = users.filter(u => u.email.toLowerCase().includes(email));
                }

                this.usuarios.set(users);
            } else {
                this.messageService.showMessageFromReponse(response);
            }
        } catch (ex: any) {
            this.messageService.showMessageFromReponse(ex.error as ApiResponse);
        } finally {
            this.isLoading = false;
        }
    }

    onGlobalFilter(table: any, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    openDialog(user: UserProfile | null = null): void {
        const onClose = this.modalService.openDialogPage(
            UsuarioDetailComponent as Type<Component>,
            user ? 'Editar Usuário' : 'Novo Usuário',
            { item: user },
            '35%'
        );

        onClose.subscribe(async (response: ApiResponse) => {
            if (response) {
                this.messageService.showMessageFromReponse(response, response.result?.data);
            }
            await this.listar();
        });
    }

    toggleStatus(user: UserProfile): void {
        const action = user.isActive ? 'desativar' : 'ativar';
        this.messageService.confirm({
            header: 'Confirmação',
            message: `Tem certeza que deseja ${action} o usuário "${user.nomeCompleto}"?`,
            accept: async () => {
                this.isLoading = true;
                try {
                    const response = user.isActive
                        ? await this.authService.deactivateUser(Number(user.id))
                        : await this.authService.activateUser(Number(user.id));
                    this.messageService.showMessageFromReponse(response, response.result?.data);
                    await this.listar();
                } catch (ex: any) {
                    this.messageService.showMessageFromReponse(ex.error);
                } finally {
                    this.isLoading = false;
                }
            }
        });
    }

    resetarSenha(user: UserProfile): void {
        this.messageService.confirm({
            header: 'Confirmação',
            message: `Deseja resetar a senha do usuário "${user.nomeCompleto}"? Um e-mail será enviado com instruções para redefinição.`,
            accept: async () => {
                this.isLoading = true;
                try {
                    const response = await this.authService.adminResetPassword(Number(user.id));
                    this.messageService.showMessageFromReponse(response);
                } catch (ex: any) {
                    this.messageService.showMessageFromReponse(ex.error);
                } finally {
                    this.isLoading = false;
                }
            }
        });
    }

    excluir(user: UserProfile): void {
        this.messageService.confirm({
            header: 'Confirmação',
            message: `Tem certeza que deseja excluir o usuário "${user.nomeCompleto}"? Esta ação é irreversível.`,
            accept: async () => {
                this.isLoading = true;
                try {
                    const response = await this.authService.deleteUser(Number(user.id));
                    this.messageService.showMessageFromReponse(response);
                    await this.listar();
                } catch (ex: any) {
                    this.messageService.showMessageFromReponse(ex.error);
                } finally {
                    this.isLoading = false;
                }
            }
        });
    }
}
