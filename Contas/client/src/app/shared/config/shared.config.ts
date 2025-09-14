import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { PanelModule } from 'primeng/panel';
import { FloatLabelModule } from 'primeng/floatlabel';
import { DividerModule } from 'primeng/divider';
import { TooltipModule } from 'primeng/tooltip';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { RippleModule } from 'primeng/ripple';
import { ButtonModule } from 'primeng/button';
import { DialogService } from 'primeng/dynamicdialog';
import { BlockUIModule } from 'primeng/blockui';
import { ConfirmationService, MessageService } from 'primeng/api';
import { MessageModule } from 'primeng/message';
import { ProgressBarModule } from 'primeng/progressbar';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { LoadingService } from '@/core/services/loading.service';
import { RouterModule } from '@angular/router';
import { BreadcrumbComponent } from '../components/breadcrumb.component';
import { ToolbarModule } from 'primeng/toolbar';

export const importedModules = [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    ToastModule,
    ConfirmDialogModule,
    PanelModule,
    FloatLabelModule,
    DividerModule,
    TooltipModule,
    TableModule,
    InputTextModule,
    IconFieldModule,
    InputIconModule,
    RippleModule,
    ButtonModule,
    MessageModule,
    BlockUIModule,
    ProgressSpinnerModule,
    ProgressBarModule,
    ToolbarModule,
];

export const importedComponents = [
    //BreadcrumbComponent,
]

export const sharedConfig = {
    imports: [...importedModules, ...importedComponents],
    exports: [...importedComponents],
    providers: [
        MessageService,
        DialogService,
        ConfirmationService,
        LoadingService,
    ]
};
