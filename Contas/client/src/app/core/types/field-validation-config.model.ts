export interface FieldValidationConfig {
    [errorKey: string]: string | ((error: any, label: string) => string);
}