import { entityResolver } from "@/core/resolvers/entity.resolver";
import { DATA_SERVICE_TOKEN } from "@/core/services/data-service.token";
import { Routes } from "@angular/router";

export default [
    { 
        path: '', 
        loadComponent: () => import('../dashboard/dashboard.component').then(m => m.DashboardComponent),
    },
    { 
        path: 'segmento-do-credor', 
        loadComponent: () => import('./segmento-do-credor/segmento-do-credor.component').then(m => m.SegmentoDoCredorComponent),
        // resolve: { entity: entityResolver },
        // providers: [
        //     { provide: DATA_SERVICE_TOKEN, useClass: DashboardService }
        // ]
    },
    {
        path: 'credor',
        loadComponent: () => import('./credor/credor.component').then(m => m.CredorComponent),
        // resolve: { entity: entityResolver },
        // providers: [
        //     { provide: DATA_SERVICE_TOKEN, useClass: CredorService }
        // ]
    },
    {
        path: 'pagador',
        loadComponent: () => import('./pagador/pagador.component').then(m => m.PagadorComponent),
        // resolve: { entity: entityResolver },
        // providers: [
        //     { provide: DATA_SERVICE_TOKEN, useClass: PagadorService }
        // ]
    },
    {
        path: 'registro-da-conta',
        loadComponent: () => import('./registro-da-conta/registro-da-conta.component').then(m => m.RegistroDaContaComponent),
        // resolve: { entity: entityResolver },
        // providers: [
        //     { provide: DATA_SERVICE_TOKEN, useClass: RegistroDaContaService }
        // ]
    }
] as Routes;