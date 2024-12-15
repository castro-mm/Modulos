import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountService } from '../services/account.service';
import { User } from '../models/user';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const accountService = inject(AccountService);

    const user: User = accountService.currentUser()!;
    if (user) {
        req = req.clone({
            headers: req.headers.append('Authorization', `Bearer ${user.token}`)
        });
    }
    return next(req);
};