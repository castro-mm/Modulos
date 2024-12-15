import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const passwordCompareValidator: ValidatorFn = (control: AbstractControl) : ValidationErrors | null => {
    return control.value.password === control.value.confirmPassword ? null : { passwordsNoMatch: true }
}