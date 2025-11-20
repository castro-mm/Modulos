import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
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
        provideHttpClient(withFetch()/*, withInterceptors([dateFormatInterceptor])*/),
        provideAnimationsAsync(),
        providePrimeNG(
            {
                theme: {
                    preset: Aura, 
                    options: { 
                        darkModeSelector: '.app-dark' 
                    } 
                },
                translation: {
                    dayNames: ['Domingo', 'Segunda-feira', 'Terça-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sábado'],
                    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb'],
                    dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S'],
                    monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    today: 'Hoje',
                    clear: 'Limpar',
                    dateFormat: 'dd/mm/yy',
                    weekHeader: 'Sem',
                    firstDayOfWeek: 0
                }            
            }
        ),
        provideEnvironmentNgxMask(),
        ...sharedConfig.providers,
        { provide: LOCALE_ID, useValue: 'pt-BR' },  // Define o locale padrão para pt-BR   

    ]
};
