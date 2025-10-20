import { provideHttpClient, withFetch } from '@angular/common/http';
import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter, withEnabledBlockingInitialNavigation, withInMemoryScrolling } from '@angular/router';
import Aura from '@primeuix/themes/aura';
import { providePrimeNG } from 'primeng/config';
import { provideEnvironmentNgxMask } from 'ngx-mask';
import { appRoutes } from './app.routes';

import { LOCALE_ID } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import localePtExtra from '@angular/common/locales/extra/pt';
import { sharedConfig } from '@/shared/config/shared.config';

registerLocaleData(localePt, 'pt-BR', localePtExtra);

export const appConfig: ApplicationConfig = {
    providers: [        
        provideRouter(
            appRoutes, 
            withInMemoryScrolling(
                { 
                    anchorScrolling: 'enabled', 
                    scrollPositionRestoration: 'enabled' 
                }
            ), 
            withEnabledBlockingInitialNavigation()
        ),
        provideHttpClient(withFetch()),
        provideAnimationsAsync(),
        providePrimeNG(
            {
                theme: {
                    preset: Aura, 
                    options: { 
                        darkModeSelector: '.app-dark' 
                    } 
                },            
            }
        ),
        provideEnvironmentNgxMask(),
        ...sharedConfig.providers,
        { provide: LOCALE_ID, useValue: 'pt-BR' },  // Define o locale padrão para pt-BR   

    ]
};
