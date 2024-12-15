import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';

export const authGuard: CanActivateFn = (route, state) => {
    const accountService = inject(AccountService);
    
    if (!accountService.currentUser()) {
        return false;        
    }
    return true;
};
