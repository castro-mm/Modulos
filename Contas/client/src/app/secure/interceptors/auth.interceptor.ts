import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthService);
    const router = inject(Router);
    const token = authService.getToken();

    let authReq = req;
    if (token) {
        authReq = authReq.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });
    }

    // Endpoints que não devem disparar logout automático em caso de 401
    const skipLogoutUrls = ['/login', '/register', '/forgot-password', '/reset-password', '/me'];
    const shouldSkipLogout = skipLogoutUrls.some(url => req.url.includes(url));
    
    return next(authReq).pipe(
        catchError((err: HttpErrorResponse) => {
            if (err.status === 401 && !shouldSkipLogout) {
                authService.logout();
            } else if (err.status === 403) {
                router.navigate(['/secure/access']);
            }
            return throwError(() => err);
        })
    );
};
