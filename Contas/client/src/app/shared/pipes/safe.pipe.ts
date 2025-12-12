import { inject, Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Pipe({
    standalone: true,
    name: 'safeUrl'
})
/**
 * @author Marcelo M. de Castro
 * @summary Pipe customizada para sanitização de URLs.
 * @description Esta pipe utiliza o DomSanitizer do Angular para sanitizar URLs que serão usadas em contextos seguros,
 * como em iframes ou links, evitando problemas de segurança relacionados a ataques XSS.
 * @version 1.0
 */
export class SafePipe implements PipeTransform {
    sanitizer = inject(DomSanitizer);    

    transform(url: string, type: string): SafeResourceUrl | string {
        if (type === 'resourceUrl') 
            return this.sanitizer.bypassSecurityTrustResourceUrl(url);
        else if (type === 'html') 
            return this.sanitizer.bypassSecurityTrustHtml(url);
        else if (type === 'style') 
            return this.sanitizer.bypassSecurityTrustStyle(url);
        else if (type === 'script') 
            return this.sanitizer.bypassSecurityTrustScript(url);

        return url;
    }
}