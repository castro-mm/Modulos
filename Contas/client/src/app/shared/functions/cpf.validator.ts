import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

/**
 * @author Marcelo M. de Castro
 * @summary Valida a estrutura do CPF
 */
export function cpfValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        if (!control.value) {
            return null;
        }

        const stringValue = String(control.value);
        const cpf = stringValue.replace(/[^\d]/g, '');

        if (cpf.length !== 11) {
            return { invalidCpf: true };
        }

        // Verifica se todos os dígitos são iguais
        if (/^(\d)\1+$/.test(cpf)) {
            return { invalidCpf: true };
        }

        // Validação do primeiro dígito verificador
        let soma = 0;
        for (let i = 0; i < 9; i++) {
            soma += parseInt(cpf.charAt(i)) * (10 - i);
        }
        let resto = soma % 11;
        const digito1 = resto < 2 ? 0 : 11 - resto;
        
        if (digito1 !== parseInt(cpf.charAt(9))) {
            return { invalidCpf: true };
        }

        // Validação do segundo dígito verificador
        soma = 0;
        for (let i = 0; i < 10; i++) {
            soma += parseInt(cpf.charAt(i)) * (11 - i);
        }
        resto = soma % 11;
        const digito2 = resto < 2 ? 0 : 11 - resto;
        
        if (digito2 !== parseInt(cpf.charAt(10))) {
            return { invalidCpf: true };
        }

        return null;
    };
}