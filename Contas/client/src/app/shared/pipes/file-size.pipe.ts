import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    standalone: true,
    name: 'fileSize'
})
/**
 * @author Marcelo M. de Castro
 * @summary Pipe customizada para formatação de tamanhos de arquivos.
 * @description Esta pipe converte um valor em bytes para uma representação legível em KB, MB, GB, etc.
 * O número de casas decimais pode ser especificado como argumento.
 * @version 1.0
 */
export class FileSizePipe implements PipeTransform {
    transform(bytes: number, decimals: number): string {
        if (bytes === 0 || bytes === null || bytes === undefined) return '0 Bytes';

        const k = 1024;
        const dm = decimals < 0 ? 0 : decimals; 
        const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];

        let i = Math.floor(Math.log(bytes) / Math.log(k));
        let result = parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
        return result;
    }
}