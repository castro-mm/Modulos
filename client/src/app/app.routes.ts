import { Routes } from '@angular/router';
import { LayoutComponent } from './layout/components/layout/layout.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { LoginComponent } from './account/components/login/login.component';
import { authGuard } from './shared/guards/auth.guard';
import { RegisterComponent } from './account/components/register/register.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    {
        path: 'secure', component: LayoutComponent, 
        canActivate: [ authGuard ],
        children: [
            { path: 'model-page', loadComponent: () => import('./model-page/model-page.component').then(c => c.ModelPageComponent) }
        ]
    },
    { path: 'notfound', component: NotFoundComponent },
    { path: '**', redirectTo: '/notfound' },
];
