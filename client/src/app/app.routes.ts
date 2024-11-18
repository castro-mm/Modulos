import { Routes } from '@angular/router';
import { LayoutComponent } from './layout/components/layout/layout.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';

export const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            { path: 'model-page', loadComponent: () => import('./model-page/model-page.component').then(c => c.ModelPageComponent) }
        ]
    },
    { path: 'notfound', component: NotFoundComponent },
    { path: '**', redirectTo: '/notfound' },
];
