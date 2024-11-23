import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';

export const authGuard: CanActivateFn = (route, state) => {
    const accountService = inject(AccountService);
    const router = inject(Router);
    
    if (!accountService.currentUser()) {
        router.navigate(['login'], { queryParams: { accessDenied: state.url }});
        console.log(state.url);
        return false;        
    }
    return true;
};
