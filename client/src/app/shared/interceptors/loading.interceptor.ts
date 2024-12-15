import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';
import { delay, finalize, identity } from 'rxjs';
import { inject } from '@angular/core';
import { BusyService } from '../services/busy.service';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
    const busyService = inject(BusyService);

    return next(req).pipe(
        environment.production ? identity : delay(500), 
        finalize(() => busyService.idle())
    )
};
