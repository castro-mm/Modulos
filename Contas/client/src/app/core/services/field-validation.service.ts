import { Injectable } from "@angular/core";
import { FieldValidationConfig } from "../types/field-validation-config.model";
import { AbstractControl, FormGroup } from "@angular/forms";

@Injectable({
    providedIn: 'root'
})
/**
 * @author Marcelo M. de Castro
 * @summary Serviço para validação de campos de formulários.
 * @description Este serviço fornece métodos para validar campos de formulários, gerenciar mensagens de erro e configurar validações personalizadas.
 */
export class FieldValidationService {
    // Configurações padrão de validação
    private defaultValidations: FieldValidationConfig = {
        required: (_, label) => `${label} é obrigatório.`,
        minlength: (error, label) => `${label} deve ter no mínimo ${error.requiredLength} caracteres. Faltam ${error.requiredLength - error.actualLength} caracteres.`,
        maxlength: (error, label) => `${label} deve ter no máximo ${error.requiredLength} caracteres. Você digitou ${error.actualLength} caracteres.`,
        email: (_, label) => `${label} deve ser um endereço de e-mail válido.`,
        pattern: (_, label) => `${label} está em um formato inválido.`,
        min: (error, label) => `${label} deve ser no mínimo ${error.min}.`,
        max: (error, label) => `${label} deve ser no máximo ${error.max}.`
    }

    // Configurações de validação personalizadas
    private customValidations: FieldValidationConfig = {};
    
    constructor() { }

    /**
     * @summary Verifica se o controle do formulário é inválido.
     * @param control O controle do formulário a ser verificado.
     * @returns boolean indicando se o controle é inválido.
     */
    isInvalid(control: AbstractControl | null): boolean {
        return !!(control && control.invalid && (control.dirty || control.touched));
    }
    
    /**
     * @summary Define validações personalizadas para o serviço.
     * @param validation Configuração de validação personalizada a ser definida.
     * @returns void
     */
    setCustomValidation(validation: FieldValidationConfig): void {
        this.customValidations = {...this.customValidations, ...validation};
    }    

    /**
     * @summary Obtém a mensagem de validação para um controle.
     * @param control O controle do formulário a ser validado.
     * @param label O rótulo do controle para mensagens de erro.
     * @returns A mensagem de validação correspondente ao controle.
     */
    getValidation(control: AbstractControl | null, label: string): string {
        if (!control || !control.errors) {
            return '';
        }

        const errorKey = Object.keys(control.errors)[0];
        const errorValue = control.errors[errorKey];

        // Prioridade: mensagens customizadas > mensagens padrão
        const validationConfig = this.customValidations[errorKey] || this.defaultValidations[errorKey];
        if (!validationConfig) {
            return `${label} é inválido.`; // Fallback genérico
        }

        if (typeof validationConfig === 'function') {
            return validationConfig(errorValue, label);
        }
        
        return validationConfig;
    }
    
    /**
     * @summary Obtém todas as validações para um formulário.
     * @param form O formulário a ser validado.
     * @param labels Um objeto contendo os rótulos dos controles do formulário.
     * @returns Um objeto contendo as mensagens de erro para cada controle inválido.
     */
    getAllValidations(form: FormGroup, labels: {[key: string]: string}): { [key: string]: string } {
        const errors: {[key: string]: string} = {};        
        
        Object.keys(form.controls).forEach((controlName: string) => {
            const control = form.get(controlName);
            const label = labels[controlName] || controlName;
            if (this.isInvalid(control)) {
                errors[controlName] = this.getValidation(control, label);
            }
        });

        return errors;
    }
}