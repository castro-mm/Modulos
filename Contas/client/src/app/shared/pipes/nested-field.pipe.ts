import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    standalone: true,
    name: 'nestedField'
})
/**
 * @author Marcelo M. de Castro
 * @summary Pipe customizada para acessar campos aninhados em objetos.
 * @description Esta pipe permite acessar valores de campos que estão aninhados dentro de objetos,
 * utilizando uma string com o caminho do campo separado por pontos (ex: "endereco.cidade.nome").
 * @version 1.0
 */
export class NestedFieldPipe implements PipeTransform {
    transform(value: any, fieldPath: string): any {
        if (!value || !fieldPath) return null;

        const fields = fieldPath.split('.');
        return fields.reduce((acc, field) => {
            return acc && acc[field] !== undefined ? acc[field] : null;
        }, value);
    }
}