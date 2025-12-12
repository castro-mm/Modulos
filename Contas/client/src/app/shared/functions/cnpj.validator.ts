import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

/**
 * @author Marcelo M. de Castro
 * @summary Valida a estrutura do CNPJ
 */
export function cnpjValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (!control.value) {
      return null;
    }

    const stringValue = String(control.value);
    const cnpj = stringValue.replace(/[^\d]/g, '');

    if (cnpj.length !== 14) {
      return { invalidCnpj: true };
    }

    // Verifica se todos os dígitos são iguais
    if (/^(\d)\1+$/.test(cnpj)) {
      return { invalidCnpj: true };
    }

    // Validação dos dígitos verificadores
    let tamanho = cnpj.length - 2;
    let numeros = cnpj.substring(0, tamanho);
    const digitos = cnpj.substring(tamanho);
    let soma = 0;
    let pos = tamanho - 7;

    for (let i = tamanho; i >= 1; i--) {
      soma += parseInt(numeros.charAt(tamanho - i)) * pos--;
      if (pos < 2) pos = 9;
    }

    let resultado = soma % 11 < 2 ? 0 : 11 - (soma % 11);
    if (resultado !== parseInt(digitos.charAt(0))) {
      return { invalidCnpj: true };
    }

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;

    for (let i = tamanho; i >= 1; i--) {
      soma += parseInt(numeros.charAt(tamanho - i)) * pos--;
      if (pos < 2) pos = 9;
    }

    resultado = soma % 11 < 2 ? 0 : 11 - (soma % 11);
    if (resultado !== parseInt(digitos.charAt(1))) {
      return { invalidCnpj: true };
    }

    return null;
  };
}