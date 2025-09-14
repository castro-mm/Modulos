import { Component } from '@angular/core';
import { sharedConfig } from '@/shared/config/shared.config';

@Component({
    selector: 'app-root',
    imports: [...sharedConfig.imports],
    template: `
        <p-toast/>
        <p-confirm-dialog/>
        <router-outlet></router-outlet>
    `,
})
export class AppComponent {}
