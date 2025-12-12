import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { BreadcrumbComponent } from '../../core/components/breadcrumb.component';
import { ToolbarModule } from 'primeng/toolbar';
import { SelectModule } from 'primeng/select';
import { IftaLabelModule } from 'primeng/iftalabel';
import { InputMaskModule } from 'primeng/inputmask';
import { TextareaModule } from 'primeng/textarea';
import { InputNumberModule } from 'primeng/inputnumber';
import { DatePickerModule } from 'primeng/datepicker';
import { FileUploadModule } from 'primeng/fileupload';
import { SafePipe } from '../pipes/safe.pipe';
import { CardModule } from 'primeng/card';
import { DialogModule } from 'primeng/dialog';
import { OverlayBadgeModule } from 'primeng/overlaybadge';
import { FieldsetModule } from 'primeng/fieldset';
import { UploadComponent } from '../../core/components/upload.component';

export const importedModules = [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    ToastModule,
    ConfirmDialogModule,
    PanelModule,
    FloatLabelModule,
    IftaLabelModule,    
    DividerModule,
    TooltipModule,
    TableModule,
    InputTextModule,
    InputMaskModule,
    IconFieldModule,
    InputIconModule,
    RippleModule,
    ButtonModule,
    MessageModule,
    BlockUIModule,
    ProgressSpinnerModule,
    ProgressBarModule,
    ToolbarModule,
    SelectModule,
    TextareaModule,
    InputNumberModule,
    DatePickerModule,
    FileUploadModule,
    ProgressBarModule,
    FormsModule, 
    DialogModule, 
    CardModule,
    OverlayBadgeModule,
    PanelModule,
    FieldsetModule,
];

export const importedComponents = [
    // UploadComponent,
    // BreadcrumbComponent,
]

/**
 * @author Marcelo M. de Castro
 * @summary Configuração compartilhada do módulo.
 * Fornece uma coleção de módulos, componentes e serviços frequentemente utilizados em toda a aplicação.
 * Facilita a importação e reutilização de funcionalidades comuns.
 * @example
 * ```typescript
 * import { Component } from '@angular/core';
 * import { sharedConfig } from '@/shared/config/shared.config';
 * 
 * @Component({
 *     imports: [...sharedConfig.imports],
 *     templateUrl: './my-component.component.html',
 *     providers: [...sharedConfig.providers]
 * })
 * export class MyComponent { }
 * ```
 */
export const sharedConfig = {
    imports: [...importedModules],    
    providers: [
        MessageService,
        DialogService,
        ConfirmationService,
        LoadingService,
    ]
};

export const sharedComponentsConfig = {
    declarations: [...importedComponents, SafePipe],
    exports: [...importedComponents, SafePipe],
};