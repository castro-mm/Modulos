import { Component, input, inject } from '@angular/core';
import { AbstractControl } from '@angular/forms';
import { sharedConfig } from '../../shared/config/shared.config';
import { FieldValidationService } from '../services/field-validation.service';

@Component({
    selector: 'app-field-validation-message',
    imports: [...sharedConfig.imports],
    template: `
        @if (validationService.isInvalid(control())) {
            <div class="mt-1">
                <p-message severity="error" variant="simple">
                    ✗ {{ validationService.getValidation(control(), label()) }}
                </p-message>
            </div>
        }
    `
})
/**
 * @author Marcelo M. de Castro
 * @summary Componente para exibir mensagens de validação de campos de formulários.
 * Utiliza o FieldValidationService para obter mensagens de erro com base no estado do controle.
 * @version 1.0.0
 * @since 2023-10-01
 */
export class FieldValidationMessageComponent {
    validationService = inject(FieldValidationService);
    
    control = input.required<AbstractControl | null>();
    label = input.required<string>();
}