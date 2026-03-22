import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';

export const roleGuard: CanActivateFn = (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    const requiredRole = route.data['role'] as string;

    if (!authService.isAuthenticated()) {
        router.navigate(['/secure/login'], { queryParams: { returnUrl: state.url } });
        return false;
    }

    if (requiredRole && authService.getUserRole() !== requiredRole) {
        router.navigate(['/secure/access']);
        return false;
    }

    return true;
};
