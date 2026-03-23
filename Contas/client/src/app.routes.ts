import { Routes } from '@angular/router';
import { AppLayout } from './app/layout/component/app.layout';
import { Landing } from './app/pages/landing/landing';
import { Notfound } from './app/pages/notfound/notfound';
import { ChangePasswordComponent } from '@/secure/components/change-password.component';
import { authGuard } from '@/secure/guards/auth.guard';

export const appRoutes: Routes = [
    {
        path: '',
        component: AppLayout,
        canActivate: [authGuard],
        children: [
            { path: '', redirectTo: 'contas', pathMatch: 'full' },
            { path: 'contas', loadChildren: () => import('@/features/features.routes') },
            { path: 'change-password', component: ChangePasswordComponent }
        ]
    },
    { path: 'secure', loadChildren: () => import('@/secure/secure.routes') },
    { path: 'landing', component: Landing },
    { path: 'notfound', component: Notfound },
    { path: 'auth', loadChildren: () => import('@/pages/auth/auth.routes') },
    { path: '**', redirectTo: '/notfound' }
];
